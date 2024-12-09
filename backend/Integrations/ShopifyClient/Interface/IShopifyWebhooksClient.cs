using POD.Integrations.ShopifyClient.Model;
using RestSharp;

namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyWebhooksClient
    {
        Task<RestResponse<Webhook>> Create(Webhook webhook);
    }
}