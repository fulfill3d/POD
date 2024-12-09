using POD.Integrations.ShopifyClient.Model;
using POD.Integrations.ShopifyClient.Model.Order;
using RestSharp;

namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyOrderFulfillmentClient
    {
        Task<RestResponse<Fulfillment>> Create(FulfillmentShipping shipping);
    }
}
