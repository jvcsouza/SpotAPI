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

        public SpotifyAlbums Album => _album ??= new SpotifyAlbums(_clientId, _clientSecret);
        private SpotifyAlbums _album;

        public SpotifyArtists Artist => _artist ??= new SpotifyArtists(_clientId, _clientSecret);
        private SpotifyArtists _artist;

        public SpotifyTracks Track => _track ??= new SpotifyTracks(_clientId, _clientSecret);
        private SpotifyTracks _track;

        public SpotifyUser User => _user ??= new SpotifyUser(_clientId, _clientSecret);
        private SpotifyUser _user;

        public SpotifyPlaylists Playlist => _playlist ??= new SpotifyPlaylists(_clientId, _clientSecret);
        private SpotifyPlaylists _playlist;

        public SpotifyClient(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }
    }
}