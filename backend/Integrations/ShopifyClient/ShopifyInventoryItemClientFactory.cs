using Microsoft.Extensions.Logging;
using POD.Integrations.ShopifyClient.Interface;

namespace POD.Integrations.ShopifyClient
{
    public class ShopifyInventoryItemClientFactory(ILogger<ShopifyInventoryItemClient> logger) : IShopifyInventoryItemClientFactory
    {
        public IShopifyInventoryItemClient CreateClient(string shop, string token)
        {
            return new ShopifyInventoryItemClient(shop, token, logger);
        }
    }
}
