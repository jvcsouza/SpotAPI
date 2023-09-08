using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpotAPI.User.Models
{
    public class ExplicitContent
    {
        [JsonProperty("filter_enabled")]
        public bool FilterEnabled { get; set; }

        [JsonProperty("filter_locked")]
        public bool FilterLocked { get; set; }
    }

    public class ExternalUrls
    {
        [JsonProperty("spotify")]
        public string Spotify { get; set; }
    }

    public class Followers
    {
        [JsonProperty("total")]
        public int Total { get; set; }
    }

    public class Image
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class SpotifyUserModel
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonProperty("followers")]
        public Followers Followers { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("images")]
        public List<Image> Images { get; set; }

        [JsonProperty("product")]
        public string Product { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }


}
