using POD.Functions.Shopify.CallExecutes.Data.Models;

namespace POD.Functions.Shopify.CallExecutes.Services.Interfaces
{
    public interface IShopifyFulfillmentService
    {
        Task<bool> CreateFulfillmentAsync(ShopifyCreateFulfillmentMessage shopifyCreateFulfillmentMessage);
        Task<bool> CreateFulfillmentRequestAsync(ShopifyFulfillmentRequestMessage shopifyFulfillmentRequestMessage);
    }
}
