using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpotAPI.Base;
using SpotAPI.Playlists.Models;
using SpotAPI.User.Models;

namespace SpotAPI.User
{
    public class SpotifyUser : SpotifyHttpClient
    {
        public SpotifyUser(string client, string secret) : base(client, secret)
        {

        }

        public Task<SpotifyUserModel> GetInfoAsync()
        {
            if (!_signed)
                throw new UnauthorizedAccessException("User not logged");

            return Execute<SpotifyUserModel>(client => client.GetAsync("me"));
        }

        public Task<List<SpotifyPlaylistsModel>> GetPlaylistsAsync()
        {
            if (!_signed)
                throw new UnauthorizedAccessException("User not logged");

            return ExecuteAsListAsync<SpotifyPlaylistsModel>("me/playlists");
        }

        public Task<SpotifyUserModel> FollowPlaylistAsync(string paylistId, bool isPublic = true)
        {
            if (!_signed)
                throw new UnauthorizedAccessException("User not logged");

            return Execute<SpotifyUserModel>(client => client.PutAsync($"playlists/{paylistId}/followers", BuildContent(new { Public = isPublic })));
        }

        public Task SignInAsync(bool waitUser = true)
        {
            return RequestUserSignIn(waitUser);
        }
    }
}
