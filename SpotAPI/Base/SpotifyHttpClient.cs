using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpotAPI.Utils;
using Spotify.SDK.Base.Models;

namespace SpotAPI.Base
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class SpotifyHttpClient
    {
        private static string _clientId;
        private static string _clientSecret;
        private string _grantCode;
        private static string _accessToken;
        protected static bool _signed;
        private const string API_URL = "https://api.spotify.com/v1/";
        private const string ACCOUNTS_URL = "https://accounts.spotify.com";
        private const string LOCAL_URL = "http://localhost:3000";

        protected SpotifyHttpClient() { }

        protected SpotifyHttpClient(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        protected void Authorize(string clientId, string clientSecret)
        {
            if(_signed || !string.IsNullOrEmpty(_accessToken))
                return;

            _clientId = clientId;
            _clientSecret = clientSecret;
            _accessToken = null;
        }

        protected async Task<string> RequestUserSignIn(bool waitUser)
        {
            if (waitUser)
                return await RequestUserSignIn();

            // ReSharper disable once AsyncVoidLambda
            var newThread = new Thread(async () => await RequestUserSignIn());

            newThread.Start();

            return string.Empty;
        }

        private async Task<string> RequestUserSignIn()
        {
            var httpListener = new HttpListener();
            httpListener.Prefixes.Add($"{LOCAL_URL}/");
            httpListener.Start();

            var dicParams = new Dictionary<string, string>
            {
                { "response_type", "code" },
                { "redirect_uri", LOCAL_URL },
                { "scope", "user-read-private playlist-read-private user-read-email user-library-modify user-library-read app-remote-control streaming user-read-playback-state user-modify-playback-state user-read-currently-playing ugc-image-upload user-follow-modify user-follow-read user-read-playback-position user-top-read user-read-recently-played playlist-modify-private playlist-modify-public" },
                { "client_id", _clientId },
            };

            var parms = string.Join("&", dicParams.Select(x => $"{x.Key}={x.Value}"));

            var psi = new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = $"https://accounts.spotify.com/authorize?{parms}"
            };
            var process = Process.Start(psi);

            var context = await httpListener.GetContextAsync();

            var request = context.Request;
            _grantCode = request.QueryString.Get(0);

            var response = context.Response;
            var buffer = Encoding.UTF8.GetBytes("<HTML><BODY> Tudo certo! Voce pode fechar essa aba.</BODY></HTML>");
            var output = response.OutputStream;
            response.ContentLength64 = buffer.Length;
            await output.WriteAsync(buffer, 0, buffer.Length);

            httpListener.Stop();
            process!.Close();

            _accessToken = null;

            await GetToken();

            return _grantCode;
        }

        private async Task<string> GetToken(bool fromSignIn = false)
        {
            if (!string.IsNullOrEmpty(_accessToken))
                return _accessToken;

            using var cliente = new HttpClient();
            var encoding = new UTF8Encoding();
            var authBasicBytes = encoding.GetBytes(_clientId + ":" + _clientSecret);
            var authBaseStr = Convert.ToBase64String(authBasicBytes);

            cliente.DefaultRequestHeaders.Clear();
            cliente.DefaultRequestHeaders.Add("Authorization", "Basic " + authBaseStr);
            cliente.DefaultRequestHeaders.Add("Accept", "application/json");

            var formDic = new Dictionary<string, string>
            {
                { "grant_type", fromSignIn ? "authorization_code" : "client_credentials" }
            };

            if (fromSignIn)
            {
                formDic.TryAdd("code", _grantCode);
                formDic.TryAdd("redirect_uri", LOCAL_URL);
            }

            var response = await cliente.PostAsync($"{ACCOUNTS_URL}/api/token", new FormUrlEncodedContent(formDic));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsAsync<SpotifyApiToken>();
                _signed = true;
                return _accessToken = content.AccessToken;
            }
            else
            {
                var contentStr = await response.Content.ReadAsStringAsync();
                var content = await response.Content.ReadAsAsync<SpotifyApiError>();
                throw new UnauthorizedAccessException(content.Error.Message);
            }
        }

        private async Task<HttpClient> CreateClient(string baseUrl)
        {
            var client = new HttpClient { BaseAddress = new Uri(baseUrl) };

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await GetToken());

            return client;
        }

        protected async Task<T> Execute<T>(Func<HttpClient, Task<HttpResponseMessage>> expression)
        {
            using var client = await CreateClient(API_URL);
            var response = await expression(client);

            if (response.IsSuccessStatusCode)
            {
                var a = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Executing {API_URL}; DataType: {typeof(T).Name};");
                return await response.Content.ReadAsAsync<T>();
            }

            var content = await response.Content.ReadAsAsync<SpotifyApiError>();

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException(content.Error.Message);

            throw new InvalidOperationException(content.Error.Message);
        }

        protected static StringContent BuildContent(object content) => new(JsonConvert.SerializeObject(content));

        protected async Task<List<T>> ExecuteAsListAsync<T>(string url, int page = 1, int size = 25, string propertyName = null)
        {
            using var client = await CreateClient(API_URL);
            var contentReturnData = new List<T>();
            var contentData = default(SpotifyListBase<T>);
            var newUrl = url + (url.Contains('?') ? "&" : "?");

            do
            {
                var response = await client.GetAsync(newUrl + $"limit={size}&offset={(page - 1) * size}");

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsAsync<SpotifyApiError>();
                    var e = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                        throw new UnauthorizedAccessException(error.Error.Message);

                    throw new InvalidOperationException(error.Error.Message);
                }

                var a = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(propertyName))
                    contentData = await response.Content.ReadAsAsync<SpotifyListBase<T>>();

                else
                {
                    var wrapContent = await response.Content.ReadAsAsync<ExpandoObject>();
                    ((IDictionary<string, object>)wrapContent).TryGetValue(propertyName, out object newContent);
                    contentData = Map.ConvertTo<SpotifyListBase<T>>(newContent);
                }

                contentReturnData.AddRange(contentData.Items);

                Console.WriteLine($"Executing {url}; Page: {page}; Size: {size}; Loaded: {contentReturnData.Count} de {contentData.Total} ({(decimal)contentReturnData.Count / (decimal)contentData.Total * 100:0.00}%);");

                page++;

            } while (contentData.Items.Count >= size);

            return contentReturnData;
        }
    }
}
