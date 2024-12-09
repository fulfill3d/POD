using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using POD.API.Seller.Payment.Data.Models.Braintree;
using POD.API.Seller.Payment.Services.Interfaces;
using POD.Common.Core.Model;
using POD.Common.Service.Interfaces;

namespace POD.API.Seller.Payment
{
    public class BraintreeFunction(
        IBraintreeService braintreeService, 
        IOptions<AuthorizationScope> opt,
        IJwtValidatorService jwtService,
        IHttpRequestBodyMapper<CompleteSetupRequest> requestBodyMapper)
    {
        private readonly AuthorizationScope _paymentScope = opt.Value;
        
        [Function(nameof(GetBraintreeClientToken))]
        public async Task<HttpResponseData> GetBraintreeClientToken(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "braintree/token")] HttpRequestData req)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _paymentScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
            
            var result = await braintreeService.GetClientToken(Guid.Parse(userRefId));
            
            response.StatusCode = HttpStatusCode.OK;
            await response.WriteStringAsync(JsonConvert.SerializeObject(result, Formatting.Indented));
            return response;
        }

        [Function(nameof(CompleteBraintreeSetup))]
        public async Task<HttpResponseData> CompleteBraintreeSetup(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "braintree/complete")] HttpRequestData req)
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
             
            var result = await braintreeService.CompleteSetup(Guid.Parse(userRefId), request);
            
            response.StatusCode = HttpStatusCode.OK;
            await response.WriteStringAsync(JsonConvert.SerializeObject(result, Formatting.Indented));
            return response;
        }
    }
}