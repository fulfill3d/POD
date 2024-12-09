using Microsoft.Extensions.Logging;
using POD.Integrations.ShopifyClient.Interface;

namespace POD.Integrations.ShopifyClient
{
    public class ShopifyAuthorizationClientFactory(ILogger<ShopifyAuthorizationClient> logger) : IShopifyAuthorizationClientFactory
    {
        public IShopifyAuthorizationClient CreateClient(string shop)
        {
            return new ShopifyAuthorizationClient(shop, logger);
        }
    }
}
