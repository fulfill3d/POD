using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Newtonsoft.Json;
using POD.Functions.Shopify.DeleteProduct.Data.Models;
using POD.Functions.Shopify.DeleteProduct.Services.Interfaces;

namespace POD.Functions.Shopify.DeleteProduct
{
    public class DeleteProductFunction(
        IDeleteProductService deleteProductService)
    {
        [Function("shopify-delete-product")]
        [OpenApiOperation(
            operationId: "ServiceBus",
            tags: new[] { "shopify-delete-product" })]
        public async Task Run(
            [ServiceBusTrigger("shopify-delete-product", Connection = "ServiceBusConnectionString",
                IsSessionsEnabled = true)]
            string message,
            FunctionContext context)
        {
            var deleteProductModel = JsonConvert.DeserializeObject<DeleteShopifyProductMessage>(message);
            await deleteProductService.DeleteProducts(deleteProductModel);
        }
    }
}