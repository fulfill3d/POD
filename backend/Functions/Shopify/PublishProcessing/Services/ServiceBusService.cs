using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using POD.Functions.Shopify.PublishProcessing.Data.Models;
using POD.Functions.Shopify.PublishProcessing.Services.Interfaces;
using POD.Integrations.ServiceBusClient.Interfaces;
using POD.Integrations.ServiceBusClient.Models;

namespace POD.Functions.Shopify.PublishProcessing.Services
{
    public class ServiceBusService(
        IServiceBusClient serviceBusClient,
        IOptions<QueueNames> opt
    ): IServiceBusService
    {
        private readonly string _callExecuteQueueName = opt.Value.ShopifyCallExecutesQueueName;

        public async Task SendCallExecuteMessage(
            ShopifyCallExecuteMessage<ShopifyProductMessage> message, string sessionId)
        {
            await serviceBusClient.SendMessage(new ServiceBusClientMessage
            {
                QueueName = _callExecuteQueueName,
                SessionId = sessionId,
                Message = JsonConvert.SerializeObject(message)
            });
        }
    }
}