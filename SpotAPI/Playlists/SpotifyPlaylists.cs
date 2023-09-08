﻿using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<SpotifyPlaylistTrackModel>> GetTracksAsync(string playlistId)
        {
            var tracks = await ExecuteAsListAsync<SpotifyPlaylistTrack>($"{ResourceName}/{playlistId}/tracks");
            return tracks.Select(x => x.Track).ToList();
        }

        public async Task<List<SpotifyPlaylistsModel>> FromUser(string userId)
        {
            return await ExecuteAsListAsync<SpotifyPlaylistsModel>($"users/{userId}/{ResourceName}");
        }
    }
}
