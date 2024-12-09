using Microsoft.Extensions.Logging;
using POD.Integrations.ShopifyClient.Interface;
using POD.Integrations.ShopifyClient.Model;
using RestSharp;

namespace POD.Integrations.ShopifyClient
{
    public class ShopifyAuthorizationClient(string shop, ILogger<ShopifyAuthorizationClient> logger) : ShopifyAuthorizationBaseClient(shop, logger), IShopifyAuthorizationClient
    {
        public async Task<RestResponse<string>> GetAccessToken(ShopifyAccessToken shopifyAccessToken)
        {
            // Use CreateRequest to prepare the request with the appropriate JSON body
            var request = CreateRequest(string.Empty, Method.Post, shopifyAccessToken);

            return await ExecuteWithPolicyAsync<string>(request);
        }
    }
}