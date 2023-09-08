using System;
using Newtonsoft.Json;
using SpotAPI.Tracks.Models;

namespace SpotAPI.Playlists.Models
{
    internal class SpotifyPlaylistTrack
    {
        [JsonProperty("added_at")]
        private DateTime AddedAt { get; set; }

        [JsonProperty("track")]
        private SpotifyPlaylistTrackModel _track;

        public SpotifyPlaylistTrackModel Track
        {
            get
            {
                _track.AddedAt = AddedAt;

                return _track;
            }
        }
    }
    
    public class SpotifyPlaylistTrackModel : SpotifyTracksModel
    {
        public DateTime AddedAt { get; set; }
    }
}
