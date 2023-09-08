using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.SDK.Base.Models
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
