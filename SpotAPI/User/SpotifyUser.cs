using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using SpotAPI.Base;
using SpotAPI.Playlists.Models;
using SpotAPI.User.Models;

namespace SpotAPI.User
{
    public class SpotifyUser : SpotifyHttpClient
    {
        public SpotifyUser(string client, string secret)
        {
            Authorize(client, secret);
        }

        public Task<SpotifyUserModel> Info()
        {
            if (!_signed)
                throw new UnauthorizedAccessException("User not logged");

            return Execute<SpotifyUserModel>(client => client.GetAsync("me"));
        }

        public Task<List<SpotifyPlaylistsModel>> Playlists()
        {
            if (!_signed)
                throw new UnauthorizedAccessException("User not logged");

            return ExecuteAsList<SpotifyPlaylistsModel>($"me/playlists");
        }

        public Task<SpotifyUserModel> FollowPlaylist(string paylistId, bool isPublic = true)
        {
            if (!_signed)
                throw new UnauthorizedAccessException("User not logged");

            return Execute<SpotifyUserModel>(client => client.PutAsync($"playlists/{paylistId}/followers", BuildContent(new { Public = isPublic })));
        }

        public Task SignIn(bool waitUser = true)
        {
            return RequestUserSignIn(waitUser);
        }
    }
}
