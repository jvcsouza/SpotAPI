using System.Collections.Generic;
using System.Threading.Tasks;
using SpotAPI.Albums.Models;
using SpotAPI.Base;
using Spotify.SDK.Base;

namespace SpotAPI.Albums
{
    public class SpotifyAlbums : SpotifyResource<SpotifyAlbumModel>
    {
        public SpotifyAlbums(string client, string secret) : base("albums")
        {
            Authorize(client, secret);
        }

        public async Task<List<SpotifyTracksModel>> Tracks(string albumId)
        {
            return await ExecuteAsList<SpotifyTracksModel>($"{_resourceName}/{albumId}/tracks");
        }

        public Task<List<SpotifyAlbumModel>> Search(string text)
        {
            return base.Search(text, "album");
        }
    }
}
