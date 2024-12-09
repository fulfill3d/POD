using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using Microsoft.Azure.Functions.Worker.Http;
using POD.Functions.Shopify.WebhookEndpoints.Data.Models;
using POD.Functions.Shopify.WebhookEndpoints.Options;
using POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces;

namespace POD.Functions.Shopify.WebhookEndpoints
{
    public class DeleteProduct(
        IProductService productService,
        IAuthService authService,
        IOptions<ShopifyRequestOptions> opt,
        IOptions<RequestTimeout> timeoutOptions)
    {
        private readonly string _shopifyDomainHeader = opt.Value.ShopifyDomainHeader;
        private readonly string _shopifyAuthenticationHeader = opt.Value.ShopifyAuthenticationHeader;
        private readonly int _timeoutMilliseconds = timeoutOptions.Value.TimeoutValue;

        [FunctionName("DeleteProduct")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "webhooks/product/delete")] HttpRequestData req,
            ILogger log)
        {
            // TODO Webhook Timeout. When webhook timeout by shopify, we still process the request so double message sent
            // Either implement a CancellationToken passing all the way through the services
            // Or Write the request message to Cache / Table, verify single message and then process it
            var cts = new CancellationTokenSource(_timeoutMilliseconds);
            var response = req.CreateResponse();
            try
            {
                log.LogInformation("Product deleted function starting.");
                
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

                await productService.DeleteProduct(requestBody, shop, cts.Token);

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
