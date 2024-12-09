using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using POD.Common.Core.Enum;
using POD.Functions.PublishSchedule.Data.Models;
using POD.Functions.PublishSchedule.Services.Interfaces;
using POD.Functions.PublishSchedule.Services.Options;
using POD.Integrations.ServiceBusClient.Interfaces;
using POD.Integrations.ServiceBusClient.Models;

namespace POD.Functions.PublishSchedule.Services
{
    public class ServiceBusService(
        IServiceBusClient serviceBusClient,
        IOptions<ServiceBusServiceOption> opt,
        ILogger<ServiceBusService> logger): IServiceBusService
    {
        private readonly string _shopifyPublishProcessQueueName = opt.Value.ShopifyStartPublishProcessQueueName;
        private readonly string _etsyPublishProcessQueueName = opt.Value.EtsyStartPublishProcessQueueName;

        public async Task SendStartPublishProcessingMessage(PublishProcessingProduct product)
        {
            switch (product.MarketPlace)
            {
                case MarketPlace.Shopify:
                    await serviceBusClient.SendMessage(new ServiceBusClientMessage
                    {
                        QueueName = _shopifyPublishProcessQueueName,
                        Message = JsonConvert.SerializeObject(product)
                    });
                    return;
                case MarketPlace.Etsy:
                    await serviceBusClient.SendMessage(new ServiceBusClientMessage
                    {
                        QueueName = _etsyPublishProcessQueueName,
                        Message = JsonConvert.SerializeObject(product)
                    });
                    return;
            }
            logger.LogInformation($"Publish message sent: {product.StoreId}-{product.StoreProductId}-{product.MarketPlace}");
        }
    }
}