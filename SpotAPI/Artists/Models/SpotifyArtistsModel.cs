using System.Collections.Generic;
using Newtonsoft.Json;
using SpotAPI.Base.Models;

namespace SpotAPI.Artists.Models
{
    public class SpotifyArtistsModel
    {
        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonProperty("followers")]
        public Followers Followers { get; set; }

        [JsonProperty("genres")]
        public List<string> Genres { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("images")]
        public List<Image> Images { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("popularity")]
        public int Popularity { get; set; }

        //[JsonProperty("type")]
        //public string Type { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }

    public class Followers
    {
        //[JsonProperty("href")]
        //public string Href { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
