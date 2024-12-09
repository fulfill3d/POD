using POD.Integrations.ShopifyClient.Model;
using RestSharp;

namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyFulfillmentServiceClient
    {
        Task<RestResponse<IEnumerable<FulfillmentService>>> GetAll();
        Task<RestResponse<FulfillmentService>> Create(FulfillmentService item);

    }
}
