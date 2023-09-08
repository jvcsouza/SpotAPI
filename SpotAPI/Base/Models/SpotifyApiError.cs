using Newtonsoft.Json;

namespace SpotAPI.Base.Models
{
    public class SpotifyApiError
    {
        [JsonProperty("error")]
        public ApiError Error { get; set; }
    }

    public class ApiError
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
