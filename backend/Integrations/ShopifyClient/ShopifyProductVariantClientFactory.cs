using Microsoft.Extensions.Logging;
using POD.Integrations.ShopifyClient.Interface;

namespace POD.Integrations.ShopifyClient
{
    public class ShopifyProductVariantClientFactory(ILogger<ShopifyProductVariantClient> logger) : IShopifyProductVariantClientFactory
    {
        public IShopifyProductVariantClient CreateClient(string shop, string token)
        {
            return new ShopifyProductVariantClient(shop, token, logger);
        }
    }
}
