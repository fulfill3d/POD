using POD.Integrations.ShopifyClient.Model;
using RestSharp;

namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyShopClient
    {
        Task<RestResponse<ShopifySeller>> GetShop();
    }
}
