using Microsoft.Extensions.Logging;
using POD.Integrations.ShopifyClient.Interface;
using POD.Integrations.ShopifyClient.Model;
using RestSharp;

namespace POD.Integrations.ShopifyClient
{
    public class ShopifyExchangeTokenClient(string shop, ILogger<ShopifyExchangeTokenClient> logger) : ShopifyAuthorizationBaseClient(shop, logger), IShopifyExchangeTokenClient
    {
        public async Task<RestResponse<string>> GetAccessToken(ExchangeToken exchangeToken)
        {
            // Use CreateRequest to prepare the request with the appropriate JSON body
            var request = CreateRequest(string.Empty, Method.Post, exchangeToken);

            return await ExecuteWithPolicyAsync<string>(request);
        }
    }
}