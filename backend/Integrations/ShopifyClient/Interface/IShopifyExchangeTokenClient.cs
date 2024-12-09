using POD.Integrations.ShopifyClient.Model;
using RestSharp;

namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyExchangeTokenClient
    {
        Task<RestResponse<string>> GetAccessToken(ExchangeToken exchangeToken);
    }
}