using System.Collections.Generic;
using System.Threading.Tasks;
using SpotAPI.Artists.Models;
using SpotAPI.Base;

namespace SpotAPI.Artists
{
    public class SpotifyArtists : SpotifyResource<SpotifyArtistsModel>
    {
        public SpotifyArtists(string client, string secret) : base("artists")
        {
            Authorize(client, secret);
        }

        public async Task<List<SpotifyAlbumModel>> GetAlbumsAsync(string artistId, int? page = null, int? size = null)
        {
            return await ExecuteAsListAsync<SpotifyAlbumModel>($"{ResourceName}/{artistId}/albums", page, size);
        }

        public Task<List<SpotifyArtistsModel>> SearchAsync(string text)
        {
            return SearchAsync(text, "artist");
        }
    }
}
