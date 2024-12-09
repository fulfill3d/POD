using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using POD.Functions.Shopify.WebhookEndpoints.Data.Models;
using POD.Functions.Shopify.WebhookEndpoints.Options;
using POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces;

namespace POD.Functions.Shopify.WebhookEndpoints
{
    public class Uninstall(
        IUninstallService uninstallService,
        IAuthService authService,
        IOptions<ShopifyRequestOptions> opt,
        IOptions<RequestTimeout> timeoutOptions)
    {
        private readonly string _shopifyDomainHeader = opt.Value.ShopifyDomainHeader;
        private readonly string _shopifyAuthenticationHeader = opt.Value.ShopifyAuthenticationHeader;
        private readonly int _timeoutMilliseconds = timeoutOptions.Value.TimeoutValue;

        [Function("Uninstall")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "webhooks/app/uninstall")] HttpRequestData req,
            ILogger log)
        {
            var cts = new CancellationTokenSource(_timeoutMilliseconds);
            var response = req.CreateResponse();
            try
            {
                log.LogInformation("Shopify Uninstall function starting.");
                
                // Extract headers
                if (!req.Headers.TryGetValues(_shopifyAuthenticationHeader, out var hmacHeaders) || 
                    !req.Headers.TryGetValues(_shopifyDomainHeader, out var shopHeaders))
                {
                    log.LogInformation("Required headers not found.");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }

                var hmac = hmacHeaders.FirstOrDefault();
                var shop = shopHeaders.FirstOrDefault();

                if (hmac == null || shop == null)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }

                string requestBody;
                using (var reader = new StreamReader(req.Body))
                {
                    requestBody = await reader.ReadToEndAsync(cts.Token);
                }

                var isWebhookTriggeredFromShopify = authService.IsAuthenticWebhook(hmac, requestBody);

                if (!isWebhookTriggeredFromShopify)
                {
                    log.LogInformation("Unauthorized request");
                    response.StatusCode = HttpStatusCode.Unauthorized;
                    return response;
                }

                var message = shop + "|-|" + requestBody;

                await uninstallService.Uninstall(message, cts.Token);

                log.LogInformation("Shopify Uninstall function finished.");
                
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
            catch (OperationCanceledException)
            {
                log.LogWarning("Request timed out.");
                response.StatusCode = HttpStatusCode.RequestTimeout;
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                cts.Dispose();
            }
        }
    }
}
