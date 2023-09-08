using System.Collections.Generic;
using Newtonsoft.Json;
using SpotAPI.Base.Models;

namespace SpotAPI.Artists.Models
{
    public class SpotifyAlbumModel
    {
        [JsonProperty("album_type")]
        public string AlbumType { get; set; }

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
        public string Uri { get; set; }

        [JsonProperty("artists")]
        public List<SpotifyArtistsModelAlbum> Artists { get; set; }
    }

    public class SpotifyArtistsModelAlbum
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}
