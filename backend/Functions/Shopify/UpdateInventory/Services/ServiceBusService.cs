using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using POD.Functions.Shopify.UpdateInventory.Data.Models;
using POD.Functions.Shopify.UpdateInventory.Services.Interfaces;
using POD.Functions.Shopify.UpdateInventory.Services.Options;
using POD.Integrations.ServiceBusClient.Interfaces;
using POD.Integrations.ServiceBusClient.Models;

namespace POD.Functions.Shopify.UpdateInventory.Services
{
    public class ServiceBusService(IServiceBusClient serviceBusClient, 
        IOptions<UpdateInventoryOptions> updateInventoryServiceOption): IServiceBusService
    {
        private readonly string _callExecuteQueueName = updateInventoryServiceOption.Value.ShopifyCallExecutesQueueName;

        public async Task SendUpdateInventoryCallExecuteMessage(string sessionId, UpdateInventoryServiceMessage message)
        {
            var serviceBusMessage = new ShopifyCallExecuteMessage<UpdateInventoryServiceMessage>
            {
                Data = message,
                MessageType = (int)POD.Common.Core.Enum.ShopifyCallExecuteMessageType.ShopifyUpdateInventoryForProduct
            };
            
            await serviceBusClient.SendMessage(new ServiceBusClientMessage
            {
                QueueName = _callExecuteQueueName,
                Message = JsonConvert.SerializeObject(serviceBusMessage),
                SessionId = sessionId,
            });
        }
    }
}