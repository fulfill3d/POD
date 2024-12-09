using POD.Functions.Shopify.CallExecutes.Data.Models;

namespace POD.Functions.Shopify.CallExecutes.Services.Interfaces
{
    public interface IServiceBusService
    {
        Task SendUpdatePublishProcessingMessage(ShopifyProductMessage message);
        Task SendPostPublishProcessingMessage(ShopifyProductMessage message);
        Task SendUpdateInventoryForProductMessage(ShopifyInventoryUpdateProduct message);
        Task SendChangeOrderStatusAsFulfilledMessage(int podOrderId);
        Task SendFulfillOrdersByCustomerMessage(ShopifyFulfillOrdersByCustomerMessage message);
    }
}
