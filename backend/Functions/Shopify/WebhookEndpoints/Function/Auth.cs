using Microsoft.Extensions.Logging;
using System.Net;
using System.Web;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces;

namespace POD.Functions.Shopify.WebhookEndpoints
{
    public class Auth(IAuthService authService)
    {

        [Function("Auth")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "webhooks/auth")] HttpRequestData req,
            ILogger log)
        {
            log.LogInformation("Shopify Auth processing started.");
            var response = req.CreateResponse();
            
            var shop = req.Query["shop"] ?? string.Empty;
            var code = req.Query["code"] ?? string.Empty;

            var queryString = HttpUtility.ParseQueryString(req.Url.Query);
            
            var isAuthenticRequest = authService.IsAuthenticRequest(queryString);

            if (!isAuthenticRequest)
            {
                log.LogInformation("Unauthorized request");
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }

            var redirectUrl = await authService.Authentication(shop, code);

            response.StatusCode = HttpStatusCode.Redirect;
            response.Headers.Add("Location", redirectUrl);
            
            log.LogInformation("Shopify Auth processing finished.");
            
            return response;
        }
    }
}
