using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Newtonsoft.Json;
using POD.Functions.Shopify.OrderProcessing.Data.Models;
using POD.Functions.Shopify.OrderProcessing.Services.Interfaces;

namespace POD.Functions.Shopify.OrderProcessing
{
    public class OrderProcessingFunction(IShopifyOrderService shopifyOrderService)
    {
        [Function("shopify-order-processing")]
        [OpenApiOperation(
            operationId: "ServiceBus",
            tags: new[] { "shopify-order-processing" })]
        public async Task Run(
            [ServiceBusTrigger("shopify-order-processing", Connection = "ServiceBusConnectionString",
                IsSessionsEnabled = true)]
            string message,
            FunctionContext context)
        {
            var orderMessage = JsonConvert.DeserializeObject<ShopifyNewOrderMessage>(message);
            var result = await shopifyOrderService.ProcessNewOrder(orderMessage);
        }
    }
}