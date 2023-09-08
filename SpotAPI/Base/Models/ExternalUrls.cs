using Newtonsoft.Json;

namespace SpotAPI.Base.Models
{
    public class ExternalUrls
    {
        [JsonProperty("spotify")]
        public string Spotify { get; set; }
    }
}
