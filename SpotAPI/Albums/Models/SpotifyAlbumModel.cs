using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;
using SpotAPI.Base.Models;
using Spotify.SDK.Base.Models;

namespace SpotAPI.Albums.Models
{
    //[AutoMap(typeof(ExpandoObject))]
    public class SpotifyAlbumModel
    {
        //[JsonProperty("album_type")]
        //public string AlbumType { get; set; }

        [JsonProperty("total_tracks")]
        public int TotalTracks { get; set; }

        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("images")]
        public List<Image> Images { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }

        [JsonProperty("uri")]
        //[SourceMember("uri")]
        public string Uri { get; set; }

        [JsonProperty("artists")]
        public List<SpotifyArtistsModel> Artists { get; set; }
    }

    public class SpotifyArtistsModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}
