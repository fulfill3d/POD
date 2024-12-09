using Microsoft.Extensions.Logging;
using POD.Integrations.ShopifyClient.Interface;
using POD.Integrations.ShopifyClient.Model;

namespace POD.Integrations.ShopifyClient
{
    public class ShopifyWebhooksClient(string token, string store, ILogger<ShopifyWebhooksClient> logger) : ShopifyBasicClient<Webhook>(token, store, "webhooks", logger), IShopifyWebhooksClient
    {
    }
}
