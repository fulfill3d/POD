using Microsoft.Extensions.Logging;
using POD.Integrations.ShopifyClient.Interface;

namespace POD.Integrations.ShopifyClient
{
    public class ShopifyLocationClientFactory(ILogger<ShopifyLocationClient> logger) : IShopifyLocationClientFactory
    {
        public IShopifyLocationClient CreateClient(string shop, string token)
        {
            return new ShopifyLocationClient(shop, token, logger);
        }
    }
}
