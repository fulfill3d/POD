using Microsoft.Extensions.Logging;
using POD.Integrations.ShopifyClient.Interface;
using POD.Integrations.ShopifyClient.Model;
using RestSharp;

namespace POD.Integrations.ShopifyClient
{
    public class ShopifyInventoryLevelClient(
        string store,
        string accessToken,
        ILogger<ShopifyInventoryLevelClient> logger)  : ShopifyBasicClient<InventoryLevel>(store, accessToken, "inventory_levels", logger), IShopifyInventoryLevelClient
    {
        private const string Path = "inventory_levels";
        private const string SetUrl = $"{Path}/set.json";
        private const string ConnectUrl = $"{Path}/connect.json";

        public async Task<RestResponse<InventoryLevel>> Set(InventoryLevel item)
        {
            var request = CreateRequest(SetUrl, Method.Post, item);

            return await ExecuteWithPolicyAsync<InventoryLevel>(request);
        }

        public async Task<RestResponse<InventoryLevel>> Connect(InventoryLevel item)
        {
            var request = CreateRequest(ConnectUrl, Method.Post, item);

            return await ExecuteWithPolicyAsync<InventoryLevel>(request);
        }
    }
}