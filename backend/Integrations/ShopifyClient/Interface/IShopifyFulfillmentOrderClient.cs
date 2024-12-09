using POD.Integrations.ShopifyClient.Model;
using RestSharp;

namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyFulfillmentOrderClient
    {
        Task<RestResponse<IEnumerable<FulfillmentOrder>>> GetFulfillmentOrdersOfOrderAsync(long orderId);
    }
}
