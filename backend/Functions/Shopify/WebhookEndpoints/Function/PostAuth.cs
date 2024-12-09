using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces;

namespace POD.Functions.Shopify.WebhookEndpoints
{
    public class PostAuth(IAuthService authService)
    {
        [Function(nameof(PostAuth))]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "webhooks/post-auth")]
            HttpRequestData req,
            ILogger log)
        {
            var response = req.CreateResponse();
            
            var state = req.Query["state"] ?? string.Empty;
            var code = req.Query["code"] ?? string.Empty;
            
            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(state))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            
            var result =  await authService.PostAuthentication(code, state);

            if (result == string.Empty)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }

            response.StatusCode = HttpStatusCode.Redirect;
            response.Headers.Add("Location", result);
            
            return response;
        }
    }
}