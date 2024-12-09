using Microsoft.Extensions.Logging;
using POD.Integrations.ShopifyClient.Interface;

namespace POD.Integrations.ShopifyClient
{
    public class ShopifyFulfillmentServiceClientFactory(ILogger<ShopifyFulfillmentServiceClient> logger) : IShopifyFulfillmentServiceClientFactory
    {
        public IShopifyFulfillmentServiceClient CreateClient(string shop, string token)
        {
            return new ShopifyFulfillmentServiceClient(shop, token, logger);
        }
    }
}
