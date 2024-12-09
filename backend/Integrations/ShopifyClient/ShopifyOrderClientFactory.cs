using Microsoft.Extensions.Logging;
using POD.Integrations.ShopifyClient.Interface;

namespace POD.Integrations.ShopifyClient
{
    public class ShopifyOrderClientFactory(ILogger<ShopifyOrderClient> logger) : IShopifyOrderClientFactory
    {
        public IShopifyOrderClient CreateClient(string shop, string token)
        {
            return new ShopifyOrderClient(shop, token, logger);
        }
    }
}
