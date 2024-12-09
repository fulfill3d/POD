using POD.Integrations.ShopifyClient.Model;
using RestSharp;

namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyFulfillmentRequestClient
    {
        Task<RestResponse<FulfillmentOrder>> CreateAsync(
            long fulfillmentOrderId, 
            FulfillmentRequest fulfillmentRequest);

        Task<RestResponse<FulfillmentOrder>> AcceptAsync(long fulfillmentOrderId);
    }
}
