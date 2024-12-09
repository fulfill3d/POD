using POD.Integrations.ShopifyClient.Model;
using RestSharp;

namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyLocationClient
    {
        Task<RestResponse<IEnumerable<Location>>> GetAll();
    }
}
