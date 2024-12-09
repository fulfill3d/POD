using Microsoft.Extensions.Logging;
using POD.Integrations.ShopifyClient.Interface;

namespace POD.Integrations.ShopifyClient
{
    public class ShopifyWebhooksClientFactory(ILogger<ShopifyWebhooksClient> logger) : IShopifyWebhooksClientFactory
    {
        public IShopifyWebhooksClient CreateClient(string shop, string token)
        {
            return new ShopifyWebhooksClient(shop, token, logger);
        }
    }
}
