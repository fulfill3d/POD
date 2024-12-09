using Microsoft.Extensions.Logging;
using POD.Integrations.ShopifyClient.Interface;

namespace POD.Integrations.ShopifyClient
{
    public class ShopifyExchangeTokenClientFactory(ILogger<ShopifyExchangeTokenClient> logger) : IShopifyExchangeTokenClientFactory
    {
        public IShopifyExchangeTokenClient CreateClient(string shop)
        {
            return new ShopifyExchangeTokenClient(shop, logger);
        }
    }
}