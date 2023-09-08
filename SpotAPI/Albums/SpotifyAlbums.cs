using System.Collections.Generic;
using System.Threading.Tasks;
using SpotAPI.Albums.Models;
using SpotAPI.Base;

namespace SpotAPI.Albums
{
    public class SpotifyAlbums : SpotifyResource<SpotifyAlbumModel>
    {
        public SpotifyAlbums(string client, string secret) : base("albums")
        {
            Authorize(client, secret);
        }

        public async Task<List<SpotifyTracksModel>> GetTracksAsync(string albumId, int? page = null, int? size = null)
        {
            return await ExecuteAsListAsync<SpotifyTracksModel>($"{ResourceName}/{albumId}/tracks", page, size);
        }

        public Task<List<SpotifyAlbumModel>> SearchAsync(string text)
        {
            return SearchAsync(text, "album");
        }
    }
}
