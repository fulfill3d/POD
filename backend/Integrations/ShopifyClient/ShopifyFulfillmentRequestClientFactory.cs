using Microsoft.Extensions.Logging;
using POD.Integrations.ShopifyClient.Interface;

namespace POD.Integrations.ShopifyClient
{
    internal class ShopifyFulfillmentRequestClientFactory(ILogger<ShopifyFulfillmentRequestClient> logger) : IShopifyFulfillmentRequestClientFactory
    {
        public IShopifyFulfillmentRequestClient CreateClient(string shop, string token)
        {
            return new ShopifyFulfillmentRequestClient(shop, token, logger);
        }
    }
}
