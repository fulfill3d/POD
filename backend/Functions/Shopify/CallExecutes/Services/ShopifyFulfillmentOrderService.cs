using Microsoft.Extensions.Logging;
using POD.Functions.Shopify.CallExecutes.Services.Interfaces;
using POD.Integrations.ShopifyClient.Interface;
using POD.Integrations.ShopifyClient.Model;

namespace POD.Functions.Shopify.CallExecutes.Services
{
    public class ShopifyFulfillmentOrderService(
        IShopifyFulfillmentOrderClientFactory shopifyFulfillmentOrderClientFactory,
        ILogger<ShopifyFulfillmentOrderService> logger) : IShopifyFulfillmentOrderService
    {
        public async Task<IEnumerable<FulfillmentOrder>> GetPodFulfillmentOrdersAsync(
            string shop, 
            string token, 
            long shopifyOrderId, 
            long podLocationId)
        {
            var shopifyFulfillmentOrderClient = shopifyFulfillmentOrderClientFactory.CreateClient(shop, token);

            var fulfillmentOrdersResponse =
                await shopifyFulfillmentOrderClient.GetFulfillmentOrdersOfOrderAsync(shopifyOrderId);

            if (!fulfillmentOrdersResponse.IsSuccessful)
            {
                logger.LogError($"Failed to get fulfillment orders for order {shopifyOrderId}");
                return null;
            }

            var podFulfillmentOrders = 
                fulfillmentOrdersResponse
                    .Data?
                    .Where(x => x.AssignedLocationId == podLocationId);

            return podFulfillmentOrders;

        }
    }
}
