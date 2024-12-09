using POD.Integrations.ShopifyClient.Model;
using RestSharp;

namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyInventoryLevelClient
    {
        Task<RestResponse<InventoryLevel>> Connect(InventoryLevel item);
        Task<RestResponse<InventoryLevel>> Set(InventoryLevel item);
    }
}
