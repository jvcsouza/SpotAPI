using SpotAPI.Base;
using SpotAPI.Tracks.Models;

namespace SpotAPI.Tracks
{
    public class SpotifyTracks : SpotifyResource<SpotifyTracksModel>
    {
        public SpotifyTracks(string client, string secret) : base("tracks")
        {
            Authorize(client, secret);
        }
    }
}
