using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using POD.API.Seller.Payment.Data.Models.Stripe;
using POD.API.Seller.Payment.Services.Interfaces;
using POD.Common.Core.Model;
using POD.Common.Service.Interfaces;

namespace POD.API.Seller.Payment
{
    public class StripeFunction(
        IStripeService stripeService, 
        IOptions<AuthorizationScope> opt,
        IJwtValidatorService jwtService,
        IHttpRequestBodyMapper<CompleteSetupRequest> requestBodyMapper)
    {
        private readonly AuthorizationScope _paymentScope = opt.Value;
        
        [Function(nameof(GetStripeSetupIntent))]
        public async Task<HttpResponseData> GetStripeSetupIntent(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "stripe/intent")] HttpRequestData req)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _paymentScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
            
            var result = await stripeService.CreateSetupIntent(Guid.Parse(userRefId));
            
            response.StatusCode = HttpStatusCode.OK;
            await response.WriteStringAsync(JsonConvert.SerializeObject(result, Formatting.Indented));
            return response;
        }

        [Function(nameof(CompleteStripeSetup))]
        public async Task<HttpResponseData> CompleteStripeSetup(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "stripe/complete")] HttpRequestData req)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _paymentScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }

            var request = await requestBodyMapper.Map(req.Body);
            
            var result = await stripeService.CompleteSetup(Guid.Parse(userRefId), request);
            
            response.StatusCode = HttpStatusCode.OK;
            await response.WriteStringAsync(JsonConvert.SerializeObject(result, Formatting.Indented));
            return response;
        }
    }
}