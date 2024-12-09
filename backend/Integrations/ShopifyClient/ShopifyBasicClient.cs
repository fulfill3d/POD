using Microsoft.Extensions.Logging;
using RestSharp;

namespace POD.Integrations.ShopifyClient
{
    public abstract class ShopifyBasicClient<T>(
        string store,
        string token,
        string path,
        ILogger logger) : ShopifyBaseClient(store, token, logger)
    {
        public async Task<RestResponse<T>> Create(T item)
        {
            var url = $"{path}.json";
            Logger.LogInformation($"Create for URL {url}");

            // Use CreateRequest to prepare the request
            var request = CreateRequest(url, Method.Post, WrapItem(item));

            return await ExecuteWithPolicyAsync<T>(request);
        }

        public async Task<RestResponse<T>> Delete(long id)
        {
            var url = $"{path}/{id}.json";
            Logger.LogInformation($"Delete for URL {url}");

            // Use CreateRequest to prepare the request
            var request = CreateRequest(url, Method.Delete);

            return await ExecuteWithPolicyAsync<T>(request);
        }

        public async Task<RestResponse<T>> Get(long id)
        {
            var url = $"{path}/{id}.json";
            Logger.LogInformation($"Get for URL {url}");

            // Use CreateRequest to prepare the request
            var request = CreateRequest(url, Method.Get);

            return await ExecuteWithPolicyAsync<T>(request);
        }

        public async Task<RestResponse<IEnumerable<T>>> GetAll()
        {
            var url = $"{path}.json?scope=all";
            Logger.LogInformation($"Get All for URL {url}");

            // Use CreateRequest to prepare the request
            var request = CreateRequest(url, Method.Get);

            return await ExecuteWithPolicyAsync<IEnumerable<T>>(request);
        }

        public async Task<RestResponse<T>> Update(long id, T item)
        {
            var url = $"{path}/{id}.json";
            Logger.LogInformation($"Update for URL {url}");

            // Use CreateRequest to prepare the request
            var request = CreateRequest(url, Method.Put, WrapItem(item));

            return await ExecuteWithPolicyAsync<T>(request);
        }

        private object WrapItem(T item)
        {
            string wrapperName = ConvertToUnderScore(typeof(T).Name).ToLower();

            var obj = new System.Dynamic.ExpandoObject();
            obj.TryAdd(wrapperName, item);

            return obj;
        }

        private static string ConvertToUnderScore(string input)
        {
            return string.Concat(input.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }
    }
}
