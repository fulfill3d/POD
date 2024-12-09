using POD.Functions.Shopify.PublishProcessing.Data.Models;

namespace POD.Functions.Shopify.PublishProcessing.Services.Interfaces
{
    public interface IServiceBusService
    {
        Task SendCallExecuteMessage(
            ShopifyCallExecuteMessage<ShopifyProductMessage> message, 
            string sessionId);
    }
}