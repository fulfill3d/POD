using POD.Functions.Shopify.OrderProcessing.Data.Models;

namespace POD.Functions.Shopify.OrderProcessing.Services.Interfaces
{
    public interface IServiceBusService
    {
        Task SendCreateFulfillmentRequestMessage(ShopifyFulfillmentRequestMessage message, int storeId);
        Task SendUpdateInventoryForProductMessage(ShopifyInventoryUpdateProduct updateInventoryForProductMessage);
    }
}
