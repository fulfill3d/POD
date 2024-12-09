using System.Text;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using POD.Common.Core.Enum;
using POD.Functions.Shopify.CallExecutes.Data.Models;
using POD.Functions.Shopify.CallExecutes.Services.Interfaces;
using POD.Integrations.ServiceBusClient.Interfaces;

namespace POD.Functions.Shopify.CallExecutes
{
    public class CallExecutesFunction(IShopifyTaskService shopifyTaskService, IServiceBusClient serviceBusClient)
    {
        [Function("shopify-call-executes")]
        public async Task Run(
            [ServiceBusTrigger(
                "shopify-call-executes",
                Connection = "ServiceBusConnectionString",
                IsSessionsEnabled = true)] ServiceBusReceivedMessage message)
        {
            var messageObj = 
                JsonConvert.DeserializeObject<ShopifyCallExecuteMessage<JObject>>(
                    Encoding.UTF8.GetString(message.Body));
            
            await shopifyTaskService.ProcessShopifyCallExecuteAsync(messageObj);
        }
    }
}