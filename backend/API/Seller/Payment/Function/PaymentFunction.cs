using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using POD.API.Seller.Payment.Services.Interfaces;
using POD.Common.Core.Model;
using POD.Common.Service.Interfaces;

namespace POD.API.Seller.Payment
{
    public class PaymentFunction(
        IPaymentService paymentService, 
        IOptions<AuthorizationScope> opt,
        IJwtValidatorService jwtService)
    {
        private readonly AuthorizationScope _paymentScope = opt.Value;
        
        [Function(nameof(GetPaymentMethods))]
        public async Task<HttpResponseData> GetPaymentMethods(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "get")] HttpRequestData req,
            ILogger log)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _paymentScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
            
            var result = await paymentService.GetCustomerPaymentMethods(Guid.Parse(userRefId));
            
            response.StatusCode = HttpStatusCode.OK;
            await response.WriteStringAsync(JsonConvert.SerializeObject(result, Formatting.Indented));
            return response;
        }

        [Function(nameof(DeletePaymentMethod))]
        public async Task<HttpResponseData> DeletePaymentMethod(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "delete/{sellerPaymentMethodId}")] HttpRequestData req,
            ILogger log,
            int sellerPaymentMethodId)
        {    
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _paymentScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
            
            var isSuccess = await paymentService.DeleteCustomerPaymentMethod(Guid.Parse(userRefId), sellerPaymentMethodId);

            if (!isSuccess)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }

        [Function(nameof(SetDefaultPaymentMethod))]
        public async Task<HttpResponseData> SetDefaultPaymentMethod(
            [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "set/default/{sellerPaymentMethodId}")] HttpRequestData req,
            ILogger log,
            int sellerPaymentMethodId)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _paymentScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
            
            var isSuccess = await paymentService.SetDefaultCustomerPaymentMethod(Guid.Parse(userRefId), sellerPaymentMethodId);

            if (!isSuccess)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        
        [Function(nameof(GetDefaultPaymentMethod))]
        public async Task<HttpResponseData> GetDefaultPaymentMethod(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "get/default")] HttpRequestData req,
            ILogger log)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _paymentScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
            
            var result = await paymentService.GetDefaultPaymentMethod(Guid.Parse(userRefId));
            
            response.StatusCode = HttpStatusCode.OK;
            await response.WriteStringAsync(JsonConvert.SerializeObject(result, Formatting.Indented));
            return response;
        }
        
    }
}