using Microsoft.Extensions.Logging;
using POD.Integrations.ShopifyClient.Interface;
using POD.Integrations.ShopifyClient.Model;

namespace POD.Integrations.ShopifyClient
{
    public class ShopifyInventoryItemClient(string token, string store, ILogger logger) : ShopifyBasicClient<InventoryItem>(token, store, "inventory_items", logger), IShopifyInventoryItemClient
    {
    }
}
