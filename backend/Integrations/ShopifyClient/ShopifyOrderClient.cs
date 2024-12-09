using Microsoft.Extensions.Logging;
using POD.Integrations.ShopifyClient.Interface;
using POD.Integrations.ShopifyClient.Model.Order;

namespace POD.Integrations.ShopifyClient
{
    public class ShopifyOrderClient(
        string store,
        string accessToken,
        ILogger<ShopifyOrderClient> logger) : ShopifyBasicClient<Order>(store, accessToken, "orders", logger), IShopifyOrderClient
    {
    }
}
