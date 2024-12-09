using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using POD.Functions.Shopify.AppInstall.Data.Models;
using POD.Functions.Shopify.AppInstall.Services.Interfaces;

namespace POD.Functions.Shopify.AppInstall
{
    public class AppInstallFunction(IAppInstallService appInstallService)
    {
        [Function("shopify-install")]
        public async Task Run([ServiceBusTrigger("shopify-install", Connection = "ServiceBusConnectionString")] ShopifyInstallMessage message, ILogger log)
        {
            log.LogInformation("Shopify New Install processing started.");
            log.LogInformation(JsonConvert.SerializeObject(message));
            
            await appInstallService.SendWelcomeMailToSeller(message);

            log.LogInformation("Shopify New Install finished.");
        }
    }
}