using Microsoft.Extensions.Logging;
using POD.Integrations.ShopifyClient.Interface;

namespace POD.Integrations.ShopifyClient
{
    public class ShopifyInventoryLevelClientFactory(ILogger<ShopifyInventoryLevelClient> logger) : IShopifyInventoryLevelClientFactory
    {
        public IShopifyInventoryLevelClient CreateClient(string shop, string token)
        {
            return new ShopifyInventoryLevelClient(shop, token, logger);
        }
    }
}
