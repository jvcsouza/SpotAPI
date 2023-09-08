using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using SpotAPI.Utils;

namespace SpotAPI.Base
{
    public class SpotifyResource<T> : SpotifyHttpClient
    {
        protected readonly string _resourceName;

        protected SpotifyResource(string resourceName)
        {
            _resourceName = resourceName;
        }

        public async Task<T> Get(string resourceId)
        {
            return await Execute<T>(client => client.GetAsync($"{_resourceName}/{resourceId}"));
        }

        public async Task<List<T>> Get(params string[] resourcesIds)
        {
            var content = await Execute<ExpandoObject>(client =>
                client.GetAsync($"{_resourceName}/?ids={string.Join(",", resourcesIds)}"));

            ((IDictionary<string, object>)content)
                .TryGetValue(_resourceName, out object contentData);

            var listData = Map.ConvertTo<List<T>>(contentData);

            return listData;
        }

        public async Task<List<T>> Get(IEnumerable<string> resourcesIds)
        {
            return await Get(resourcesIds.ToArray());
        }

        protected virtual async Task<List<T>> Search(string text, string resource)
        {
            return await ExecuteAsList<T>($"search?query={text}&type={resource}", 1, 50, _resourceName);
        }
    }
}
