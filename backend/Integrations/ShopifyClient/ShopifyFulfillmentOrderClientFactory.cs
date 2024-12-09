using Microsoft.Extensions.Logging;
using POD.Integrations.ShopifyClient.Interface;

namespace POD.Integrations.ShopifyClient
{
    public class ShopifyFulfillmentOrderClientFactory(ILogger<ShopifyFulfillmentOrderClient> logger) : IShopifyFulfillmentOrderClientFactory
    {
        public IShopifyFulfillmentOrderClient CreateClient(string shop, string token)
        {
            return new ShopifyFulfillmentOrderClient(shop, token, logger);
        }
    }
}
