using POD.Integrations.ShopifyClient.Model.Order;
using RestSharp;

namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyOrderClient
    {
        Task<RestResponse<Order>> Get(long shopifyOrderId);
    }
}
