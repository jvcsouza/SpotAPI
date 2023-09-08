using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using SpotAPI.Utils;

namespace SpotAPI.Base
{
    public class SpotifyResource<T> : SpotifyHttpClient
    {
        protected readonly string ResourceName;

        protected SpotifyResource(string resourceName)
        {
            ResourceName = resourceName;
        }

        public async Task<T> GetAsync(string resourceId)
        {
            return await Execute<T>(client => client.GetAsync($"{ResourceName}/{resourceId}"));
        }

        public async Task<List<T>> GetAsync(params string[] resourcesIds)
        {
            var content = await Execute<ExpandoObject>(client =>
                client.GetAsync($"{ResourceName}/?ids={string.Join(",", resourcesIds)}"));

            ((IDictionary<string, object>)content)
                .TryGetValue(ResourceName, out object contentData);

            var listData = Map.ConvertTo<List<T>>(contentData);

            return listData;
        }

        public async Task<List<T>> GetAsync(IEnumerable<string> resourcesIds)
        {
            return await GetAsync(resourcesIds.ToArray());
        }

        protected virtual async Task<List<T>> SearchAsync(string text, string resource)
        {
            return await ExecuteAsListAsync<T>($"search?query={text}&type={resource}", 1, 50, false, ResourceName);
        }
    }
}
