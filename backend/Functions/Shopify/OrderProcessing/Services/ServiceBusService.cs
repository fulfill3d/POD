using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using POD.Common.Core.Enum;
using POD.Functions.Shopify.OrderProcessing.Data.Models;
using POD.Functions.Shopify.OrderProcessing.Services.Interfaces;
using POD.Functions.Shopify.OrderProcessing.Services.Options;
using POD.Integrations.ServiceBusClient.Interfaces;
using POD.Integrations.ServiceBusClient.Models;

namespace POD.Functions.Shopify.OrderProcessing.Services
{
    public class ServiceBusService(
        IServiceBusClient serviceBusClient,
        IOptions<ServiceBusQueueNames> serviceBusOpt) : IServiceBusService
    {
        private readonly ServiceBusQueueNames _serviceBusQueueNames = serviceBusOpt.Value;

        public async Task SendCreateFulfillmentRequestMessage(ShopifyFulfillmentRequestMessage message, int storeId)
        {
            var shopifyMessage = new ShopifyCallExecuteMessage<ShopifyFulfillmentRequestMessage>
            {
                Data = message,
                MessageType = (int)ShopifyCallExecuteMessageType.ShopifyCreateFulfillmentRequest
            };
            
            await serviceBusClient.SendMessage(
                new ServiceBusClientMessage
                {
                    Message = JsonConvert.SerializeObject(shopifyMessage),
                    SessionId = storeId.ToString(),
                    QueueName = _serviceBusQueueNames.ShopifyCallExecutesQueueName
                });

        }

        public async Task SendUpdateInventoryForProductMessage(
            ShopifyInventoryUpdateProduct updateInventoryForProductMessage)
        {
            await serviceBusClient.SendMessage(
                new ServiceBusClientMessage
                {
                    Message = JsonConvert.SerializeObject(updateInventoryForProductMessage),
                    QueueName = _serviceBusQueueNames.ShopifyUpdateInventoryQueueName
                });
        }
    }
}
