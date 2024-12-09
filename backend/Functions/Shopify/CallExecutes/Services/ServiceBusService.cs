using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using POD.Functions.Shopify.CallExecutes.Data.Models;
using POD.Functions.Shopify.CallExecutes.Services.Interfaces;
using POD.Functions.Shopify.CallExecutes.Services.Options;
using POD.Integrations.ServiceBusClient.Interfaces;
using POD.Integrations.ServiceBusClient.Models;

namespace POD.Functions.Shopify.CallExecutes.Services
{
    public class ServiceBusService(
        IServiceBusClient serviceBusClient,
        IOptions<QueueNames> queueNamesOpt,
        ILogger<ServiceBusService> logger) : IServiceBusService
    {
        private readonly QueueNames _queueNames = queueNamesOpt.Value;

        public async Task SendUpdatePublishProcessingMessage(ShopifyProductMessage message)
        {
            await serviceBusClient.SendMessage(new ServiceBusClientMessage
            {
                Message = JsonConvert.SerializeObject(message),
                QueueName = _queueNames.ShopifyUpdatePublishProcessQueueName
            });
        }

        public async Task SendPostPublishProcessingMessage(ShopifyProductMessage message)
        {
            await serviceBusClient.SendMessage(new ServiceBusClientMessage
            {
                Message = JsonConvert.SerializeObject(message),
                QueueName = _queueNames.ShopifyPostPublishProcessQueueName
            });
        }

        public async Task SendUpdateInventoryForProductMessage(ShopifyInventoryUpdateProduct message)
        {
            await serviceBusClient.SendMessage(new ServiceBusClientMessage
            {
                Message = JsonConvert.SerializeObject(message),
                SessionId = message.StoreId.ToString(),
                QueueName = _queueNames.ShopifyUpdateInventoryQueueName
            });
        }

        public async Task SendChangeOrderStatusAsFulfilledMessage(int podOrderId)
        {
            var message =
                new ChangeOrderStatusAsFulfilledMessage
                {
                    PodOrderId = podOrderId
                };
                
            await serviceBusClient.SendMessage(new ServiceBusClientMessage
            {
                Message = JsonConvert.SerializeObject(message),
                QueueName = _queueNames.ChangeOrderStatusAsFulfilledQueueName
            });
        }

        public async Task SendFulfillOrdersByCustomerMessage(ShopifyFulfillOrdersByCustomerMessage message)
        {
            await serviceBusClient.SendMessage(new ServiceBusClientMessage
            {
                Message = JsonConvert.SerializeObject(message),
                QueueName = _queueNames.ShopifyFulfillOrdersByCustomerQueueName,
                SessionId = message.CustomerId.ToString()
            });
        }
    }
}
