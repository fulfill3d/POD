using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces;

namespace POD.Functions.Shopify.WebhookEndpoints
{
    public class Install(IInstallService installService, ISellerService sellerService)
    {
        [Function("Install")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "webhooks/app/install")] HttpRequestData req,
            ILogger log)
        {
            var response = req.CreateResponse();
            var shop = req.Query["shop"] ?? string.Empty;

            log.LogInformation("Shopify Install processing started.");
            log.LogInformation(JsonConvert.SerializeObject(shop));

            if (shop == string.Empty)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            
            // var blockedCustomer = await sellerService.IsSellerBlocked(shop);
            // if (blockedCustomer)
            // {
            //     log.LogInformation($"Blocked Customer {shop}");
            //     return new StatusCodeResult((int)HttpStatusCode.Unauthorized);
            // }

            var result = await installService.Install(shop);

            if (result.IsContentReturn)
            {
                response.StatusCode = HttpStatusCode.OK;
                await response.WriteStringAsync(result.Result);
                return response;
            }

            log.LogInformation("Shopify New Install finished.");

            response.StatusCode = HttpStatusCode.Redirect;
            response.Headers.Add("Location", result.Result);
            
            log.LogInformation("Shopify Auth processing finished.");
            
            return response;
            
        }
    }
}
