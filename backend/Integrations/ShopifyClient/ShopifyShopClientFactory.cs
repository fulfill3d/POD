using Microsoft.Extensions.Logging;
using POD.Integrations.ShopifyClient.Interface;

namespace POD.Integrations.ShopifyClient
{
    public class ShopifyShopClientFactory(ILogger<ShopifyShopClient> logger) : IShopifyShopClientFactory
    {
        public IShopifyShopClient CreateClient(string shop, string token)
        {
            return new ShopifyShopClient(shop, token, logger);
        }
    }
}
