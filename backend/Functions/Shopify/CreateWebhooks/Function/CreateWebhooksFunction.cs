using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Newtonsoft.Json;
using POD.Functions.Shopify.CreateWebhooks.Data.Models;
using POD.Functions.Shopify.CreateWebhooks.Services.Interfaces;

namespace POD.Functions.Shopify.CreateWebhooks
{
    public class CreateWebhooksFunction(
        ICreateWebhooksService createWebhooksService)
    {

        [Function("shopify-create-webhooks")]
        [OpenApiOperation(
            operationId: "ServiceBus",
            tags: new[] { "shopify-create-webhooks" })]
        public async Task Run(
            [ServiceBusTrigger("shopify-create-webhooks", Connection = "ServiceBusConnectionString",
                IsSessionsEnabled = true)]
            string message,
            FunctionContext context)
        {
            var createWebhookModel = JsonConvert.DeserializeObject<CreateWebhookMessage>(message);
            await createWebhooksService.RegisterWebhooks(createWebhookModel);
        }
    }
}