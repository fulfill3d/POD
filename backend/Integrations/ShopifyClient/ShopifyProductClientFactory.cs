using Microsoft.Extensions.Logging;
using POD.Integrations.ShopifyClient.Interface;

namespace POD.Integrations.ShopifyClient
{
    public class ShopifyProductClientFactory(ILogger<ShopifyProductClient> logger) : IShopifyProductClientFactory
    {
        public IShopifyProductClient CreateClient(string shop, string token)
        {
            return new ShopifyProductClient(shop, token, logger);
        }
    }
}
