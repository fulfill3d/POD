using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Newtonsoft.Json;
using POD.Functions.Shopify.UpdateInventory.Data.Models;
using POD.Functions.Shopify.UpdateInventory.Services.Interfaces;

namespace POD.Functions.Shopify.UpdateInventory
{
    public class UpdateInventoryFunction(
        IUpdateInventoryService updateInventoryService)
    {
        [Function("shopify-update-inventory")]
        [OpenApiOperation(
            operationId: "ServiceBus",
            tags: new[] { "shopify-update-inventory" })]
        public async Task ServiceBus(
            [ServiceBusTrigger("shopify-update-inventory", Connection = "ServiceBusConnectionString",
                IsSessionsEnabled = true)]
            string message,
            FunctionContext context)
        {
            var product = JsonConvert.DeserializeObject<UpdateInventoryProduct>(message);
            await updateInventoryService.CallUpdateInventory(product);
        }
    }
}