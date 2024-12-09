using Microsoft.Extensions.Logging;
using POD.Integrations.ShopifyClient.Interface;

namespace POD.Integrations.ShopifyClient
{
    public class ShopifyOrderFulfillmentClientFactory(ILogger<ShopifyOrderFulfillmentClient> logger) : IShopifyOrderFulfillmentClientFactory
    {
        public IShopifyOrderFulfillmentClient CreateClient(string shop, string token)
        {
            return new ShopifyOrderFulfillmentClient(shop, token, logger);
        }
    }
}
