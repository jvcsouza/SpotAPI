using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpotAPI.Utils
{
    public static class HttpContentExtesions
    {
        public static async Task<T> ReadAsAsync<T>(this HttpContent response)
        {
            var json = await response.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }
     }
}
