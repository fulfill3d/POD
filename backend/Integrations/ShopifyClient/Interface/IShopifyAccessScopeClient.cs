using RestSharp;
using POD.Integrations.ShopifyClient.Model;

namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyAccessScopeClient
    {
        Task<RestResponse<IEnumerable<AccessScope>>> GetAll();
    }
}
