using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using SpotAPI.Base;
using SpotAPI.Playlists.Models;
using SpotAPI.Tracks.Models;

namespace SpotAPI.Playlists
{
    public class SpotifyPlaylists : SpotifyResource<SpotifyPlaylistsModel>
    {
        public SpotifyPlaylists(string client, string secret) : base("playlists")
        {
            Authorize(client, secret);
        }

        public async Task<List<SpotifyTracksModel>> Tracks(string playlistId)
        {
            return await ExecuteAsList<SpotifyTracksModel>($"{_resourceName}/{playlistId}/tracks");
        }

        public async Task<List<SpotifyPlaylistsModel>> FromUser(string userId)
        {
            return await ExecuteAsList<SpotifyPlaylistsModel>($"users/{userId}/{_resourceName}");
        }
    }
}
