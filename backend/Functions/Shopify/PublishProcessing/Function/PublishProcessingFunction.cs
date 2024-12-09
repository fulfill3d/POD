using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Newtonsoft.Json;
using POD.Functions.Shopify.PublishProcessing.Data.Models;
using POD.Functions.Shopify.PublishProcessing.Services.Interfaces;

namespace POD.Functions.Shopify.PublishProcessing
{
    public class PublishProcessingFunction(
        IPublishProcessingService publishProcessingService)
    {
        [Function("shopify-start-publish-processing")]
        [OpenApiOperation(
            operationId: "ServiceBus",
            tags: new[] { "shopify-start-publish-processing" })]
        public async Task StartPublishProcessing(
            [ServiceBusTrigger("shopify-start-publish-processing", Connection = "ServiceBusConnectionString",
                IsSessionsEnabled = true)]
            string message,
            FunctionContext context)
        {
            var product = JsonConvert.DeserializeObject<PublishProductMessage>(message);
            await publishProcessingService.StartPublishProcessingCallExecute(product);
        }
        
        [Function("shopify-update-publish-processing")]
        [OpenApiOperation(
            operationId: "ServiceBus",
            tags: new[] { "shopify-update-publish-processing" })]
        public async Task UpdatePublishProcessing(
            [ServiceBusTrigger("shopify-update-publish-processing", Connection = "ServiceBusConnectionString",
                IsSessionsEnabled = true)]
            string message,
            FunctionContext context)
        {
            var product = JsonConvert.DeserializeObject<ShopifyProductMessage>(message);
            await publishProcessingService.UpdatePublishProcessingCallExecute(product);
        }
        
        [Function("shopify-post-publish-processing")]
        [OpenApiOperation(
            operationId: "ServiceBus",
            tags: new[] { "shopify-post-publish-processing" })]
        public async Task PostPublishProcessing(
            [ServiceBusTrigger("shopify-post-publish-processing", Connection = "ServiceBusConnectionString",
                IsSessionsEnabled = true)]
            string message,
            FunctionContext context)
        {
            var product = JsonConvert.DeserializeObject<ShopifyProductMessage>(message);
            await publishProcessingService.PostPublishProcessingCallExecute(product);
        }
    }
}