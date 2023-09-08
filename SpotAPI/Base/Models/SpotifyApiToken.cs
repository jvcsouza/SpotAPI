using Newtonsoft.Json;

namespace SpotAPI.Base.Models
{
    internal class SpotifyApiToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresAt { get; set; }
    }
}
