using System.Net;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using POD.API.Seller.Address.Services.Interfaces;
using POD.API.Seller.Common.Services.Interfaces;
using POD.Common.Core.Model;
using POD.Common.Service.Interfaces;

namespace POD.API.Seller.Address
{
    public class AddressFunction(
        ICommonSellerService sellerService,
        ILogger<AddressFunction> logger,
        IAddressService addressService,
        IJwtValidatorService jwtService,
        IOptions<AuthorizationScope> opt,
        IHttpRequestBodyMapper<Data.Models.Address> aRequestBodyMapper)
    {
        private readonly AuthorizationScope _addressScope = opt.Value;
        
        [Function(nameof(GetAddresses))]
        public async Task<HttpResponseData> GetAddresses(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "get")]
            HttpRequestData req,
            ILogger log)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _addressScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
                
            var sellerId = await sellerService.GetSellerIdByUserId(Guid.Parse(userRefId));

            if (sellerId == 0)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }
            
            var addresses = await addressService.GetAddresses(sellerId);
            
            response.StatusCode = HttpStatusCode.OK;
            await response.WriteStringAsync(JsonConvert.SerializeObject(addresses, Formatting.Indented));
            return response;
        }

        [Function(nameof(GetAddress))]
        public async Task<HttpResponseData> GetAddress(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "get/{sellerAddressId}")]
            HttpRequestData req,
            int sellerAddressId)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _addressScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
                
            var sellerId = await sellerService.GetSellerIdByUserId(Guid.Parse(userRefId));

            if (sellerId == 0)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }

            var address = await addressService.GetAddress(sellerId, sellerAddressId);
            
            if (address == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }
            
            response.StatusCode = HttpStatusCode.OK;
            await response.WriteStringAsync(JsonConvert.SerializeObject(address, Formatting.Indented));
            return response;
        }

        [Function(nameof(AddAddress))]
        public async Task<HttpResponseData> AddAddress(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "add")]
            HttpRequestData req,
            ILogger log)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _addressScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
                
            var sellerId = await sellerService.GetSellerIdByUserId(Guid.Parse(userRefId));

            if (sellerId == 0)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }

            var request = await aRequestBodyMapper.Map(req.Body);
            
            var result = await addressService.AddAddress(sellerId, request);
            
            response.StatusCode = HttpStatusCode.OK;
            await response.WriteStringAsync(JsonConvert.SerializeObject(result, Formatting.Indented));
            return response;
        }

        [Function(nameof(UpdateAddress))]
        public async Task<HttpResponseData> UpdateAddress(
            [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "update")]
            HttpRequestData req,
            ILogger log)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _addressScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
                
            var sellerId = await sellerService.GetSellerIdByUserId(Guid.Parse(userRefId));

            if (sellerId == 0)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }

            var request = await aRequestBodyMapper.Map(req.Body);
            
            var result = await addressService.UpdateAddress(sellerId, request);
            
            response.StatusCode = HttpStatusCode.OK;
            await response.WriteStringAsync(JsonConvert.SerializeObject(result, Formatting.Indented));
            return response;
        }
        
        [Function(nameof(DeleteAddress))]
        public async Task<HttpResponseData> DeleteAddress(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "delete/{sellerAddressId}")]
            HttpRequestData req,
            int sellerAddressId)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _addressScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
                
            var sellerId = await sellerService.GetSellerIdByUserId(Guid.Parse(userRefId));

            if (sellerId == 0)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }

            var isSuccess = await addressService.DeleteAddress(sellerId, sellerAddressId);

            if (!isSuccess)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
    }
}