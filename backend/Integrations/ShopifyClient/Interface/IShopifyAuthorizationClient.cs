using POD.Integrations.ShopifyClient.Model;
using RestSharp;

namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyAuthorizationClient
    {
        Task<RestResponse<string>> GetAccessToken(ShopifyAccessToken shopifyAccessToken);
    }
}
