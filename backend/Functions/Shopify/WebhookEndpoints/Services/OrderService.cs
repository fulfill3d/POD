using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using POD.Functions.Shopify.WebhookEndpoints.Data.Models;
using POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces;
using POD.Integrations.ShopifyClient.Model.Order;

namespace POD.Functions.Shopify.WebhookEndpoints.Services
{
    public class OrderService(
        IServiceBusService serviceBusService,
        IBlobService blobService,
        ILogger<OrderService> logger) : IOrderService
    {

        public async Task OrderCreated(Order order, string shop, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation($"new order request {shop}");

                var shopOrder = new ShopOrder
                {
                    Shop = shop,
                    Order = order
                };

                await serviceBusService.SendOrderCreateMessageToServiceBus(shopOrder, shop + order.Id, cancellationToken);
            }
            catch (Exception ex)
            {
                var uniqueName = Guid.NewGuid();

                logger.LogError("order created error", ex.Message);

                await blobService.SaveShopifyOrderError(uniqueName.ToString(), JsonConvert.SerializeObject(order));
            }
        }
        public async Task OrderDeleted(Order order, string shop, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation($"order delete request {shop}");

                var deletedOrder = new ShopOrder
                {
                    Shop = shop,
                    Order = order
                };

                await serviceBusService.SendDeleteOrderMessageToServiceBus(deletedOrder,cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
