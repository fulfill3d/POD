using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using POD.Functions.Shopify.WebhookEndpoints.Data.Models;
using POD.Functions.Shopify.WebhookEndpoints.Service.Options;
using POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces;
using POD.Integrations.ServiceBusClient.Interfaces;
using POD.Integrations.ServiceBusClient.Models;

namespace POD.Functions.Shopify.WebhookEndpoints.Services
{
    public class ServiceBusService(
        IServiceBusClient serviceBusClient,
        IOptions<ShopifyApiEndpointsOptions> opt) : IServiceBusService
    {
        private readonly string _newShopifyOrderProcessingServiceBusName = opt.Value.ShopifyOrderProcessingServiceBusName;
        private readonly string _newShopifyInstallServiceBusName = opt.Value.ShopifyInstallServiceBusName;
        private readonly string _shopifyUninstallServiceBusName = opt.Value.ShopifyUninstallServiceBusName;
        private readonly string _shopifyStoreStatusUpdateQueueName = opt.Value.ShopifyStoreStatusUpdateQueueName;
        private readonly string _shopifyDeleteProductsServiceBusName = opt.Value.ShopifyDeleteProductsServiceBusName;
        private readonly string _shopifyOrdersDeletedServiceBusName = opt.Value.ShopifyOrdersDeletedServiceBusName;
        private readonly string _shopifyCreateWebhooksServiceBusName = opt.Value.ShopifyCreateWebhooksServiceBusName;

        public async Task SendOrderCreateMessageToServiceBus(ShopOrder shopOrder, string sessionId, CancellationToken cancellationToken)
        {
            var message = new ServiceBusClientMessage
            {
                Message = JsonConvert.SerializeObject(shopOrder),
                SessionId = sessionId,
                QueueName = _newShopifyOrderProcessingServiceBusName
            };
            
            await serviceBusClient.SendMessageWithTimeout(message, cancellationToken);
        }

        public async Task SendInstallMessageToServiceBus(NewInstallQueueMessage newInstall)
        {
            var message = new ServiceBusClientMessage
            {
                Message = JsonConvert.SerializeObject(newInstall),
                QueueName = _newShopifyInstallServiceBusName
            };

            await serviceBusClient.SendMessage(message);
        }

        public async Task SendUninstallMessageToServiceBus(string message, CancellationToken cancellationToken)
        {
            await serviceBusClient.SendMessageWithTimeout(new ServiceBusClientMessage
            {
                Message = message,
                QueueName = _shopifyUninstallServiceBusName
            }, cancellationToken);
        }

        public async Task SendUpdateShopMessageToServiceBus(string message, CancellationToken cancellationToken)
        {
            await serviceBusClient.SendMessageWithTimeout(new ServiceBusClientMessage
            {
                Message = message,
                QueueName = _shopifyStoreStatusUpdateQueueName
            }, cancellationToken);
        }

        public async Task SendDeleteProductMessageToServiceBus(DeleteProduct deleteProduct, CancellationToken cancellationToken)
        {
            var message = new ServiceBusClientMessage
            {
                Message = JsonConvert.SerializeObject(deleteProduct),
                QueueName = _shopifyDeleteProductsServiceBusName
            };

            await serviceBusClient.SendMessageWithTimeout(message, cancellationToken);
        }

        public async Task SendDeleteOrderMessageToServiceBus(ShopOrder shopOrder, CancellationToken cancellationToken)
        {
            var message = new ServiceBusClientMessage
            {
                Message = JsonConvert.SerializeObject(shopOrder),
                QueueName = _shopifyOrdersDeletedServiceBusName
            };

            await serviceBusClient.SendMessageWithTimeout(message, cancellationToken);
        }

        public async Task SendCreateWebhookMessageToServiceBus(CreateWebhook createWebhook)
        {
            var message = new ServiceBusClientMessage
            {
                Message = JsonConvert.SerializeObject(createWebhook),
                QueueName = _shopifyCreateWebhooksServiceBusName
            };

            await serviceBusClient.SendMessage(message);
        }
    }
}