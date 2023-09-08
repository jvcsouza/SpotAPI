using SpotAPI.Albums;
using SpotAPI.Artists;
using SpotAPI.Playlists;
using SpotAPI.Tracks;
using SpotAPI.User;

namespace SpotAPI
{
    public class SpotifyClient
    {
        private readonly string _clientId;
        private readonly string _clientSecret;

        public SpotifyAlbums Albums => _albums ??= new SpotifyAlbums(_clientId, _clientSecret);
        private SpotifyAlbums _albums;

        public SpotifyArtists Artists => _artists ??= new SpotifyArtists(_clientId, _clientSecret);
        private SpotifyArtists _artists;

        public SpotifyTracks Tracks => _tracks ??= new SpotifyTracks(_clientId, _clientSecret);
        private SpotifyTracks _tracks;

        public SpotifyUser User => _user ??= new SpotifyUser(_clientId, _clientSecret);
        private SpotifyUser _user;

        public SpotifyPlaylists Playlists => _playlist ??= new SpotifyPlaylists(_clientId, _clientSecret);
        private SpotifyPlaylists _playlist;

        public SpotifyClient(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }
    }
}