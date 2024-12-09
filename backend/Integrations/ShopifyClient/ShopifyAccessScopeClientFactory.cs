using Microsoft.Extensions.Logging;
using POD.Integrations.ShopifyClient.Interface;

namespace POD.Integrations.ShopifyClient
{
    public class ShopifyAccessScopeClientFactory(ILogger<ShopifyAccessScopeClient> logger) : IShopifyAccessScopeClientFactory
    {
        public IShopifyAccessScopeClient CreateClient(string shop, string token)
        {
            return new ShopifyAccessScopeClient(shop, token, logger);
        }
    }
}
