using POD.Functions.Shopify.PublishProcessing.Data.Models;

namespace POD.Functions.Shopify.PublishProcessing.Services.Interfaces
{
    public interface IPublishProcessingService
    {
        Task StartPublishProcessingCallExecute(PublishProductMessage message);
        Task UpdatePublishProcessingCallExecute(ShopifyProductMessage message);
        Task PostPublishProcessingCallExecute(ShopifyProductMessage message);
    }
}