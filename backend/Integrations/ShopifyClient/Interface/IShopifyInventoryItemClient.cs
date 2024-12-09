using POD.Integrations.ShopifyClient.Model;
using RestSharp;

namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyInventoryItemClient
    {
        Task<RestResponse<InventoryItem>> Update(long id, InventoryItem item);
    }
}
