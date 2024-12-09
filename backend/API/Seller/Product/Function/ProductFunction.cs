using System.Collections.Specialized;
using System.Net;
using System.Web;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using POD.API.Common.Core;
using POD.API.Seller.Common.Services.Interfaces;
using POD.API.Seller.Product.Data.Models;
using POD.API.Seller.Product.Services.Interfaces;
using POD.Common.Core.Model;
using POD.Common.Service.Interfaces;

namespace POD.API.Seller.Product
{
    public class ProductFunction( 
        IOptions<AuthorizationScope> opt,
        IJwtValidatorService jwtService,
        ICommonSellerService sellerService,
        IProductService productService, 
        IHttpRequestBodyMapper<ProductRequest> pRequestMapper,
        IHttpRequestBodyMapper<ProductVariantRequest> pVariantRequestMapper,
        IMapper<NameValueCollection, Pagination> paginationParametersMapper)
    {
        private readonly AuthorizationScope _productScope = opt.Value;
        
        [Function("GetProducts")]
        public async Task<HttpResponseData> GetProducts(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "get")]
            HttpRequestData req)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _productScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
            
            var queryParameters = HttpUtility.ParseQueryString(req.Query.ToString() ?? string.Empty);
            var pagination = paginationParametersMapper.Map(queryParameters);
                
            var result = await productService.GetProducts(Guid.Parse(userRefId), pagination);
            
            response.StatusCode = HttpStatusCode.OK;
            await response.WriteStringAsync(JsonConvert.SerializeObject(result, Formatting.Indented));
            return response;
        }
        
        [Function("GetProduct")]
        public async Task<HttpResponseData> GetProduct(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "get/{sellerProductId}")]
            HttpRequestData req, int sellerProductId)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _productScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
            
            var result = await productService.GetProduct(Guid.Parse(userRefId), sellerProductId);
            
            response.StatusCode = HttpStatusCode.OK;
            await response.WriteStringAsync(JsonConvert.SerializeObject(result, Formatting.Indented));
            return response;
        }
        
        [Function("CreateProduct")]
        public async Task<HttpResponseData> CreateProduct(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "create")]
            HttpRequestData req)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _productScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
            
            var sellerId = await sellerService.GetSellerIdByUserId(Guid.Parse(userRefId));
                
            var request = await pRequestMapper.Map(req.Body);
            
            var isSuccess = await productService.CreateProduct(Guid.Parse(userRefId), sellerId, request);

            if (!isSuccess)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        
        [Function("UpdateProduct")]
        public async Task<HttpResponseData> UpdateProduct(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "update")]
            HttpRequestData req)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _productScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
            
            var request = await pRequestMapper.Map(req.Body);
            
            var isSuccess = await productService.UpdateProduct(Guid.Parse(userRefId), request);

            if (!isSuccess)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        
        [Function("DeleteProduct")]
        public async Task<HttpResponseData> DeleteProduct(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "delete/{sellerProductId}")]
            HttpRequestData req, int sellerProductId)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _productScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
            
            var isSuccess = await productService.DeleteProduct(Guid.Parse(userRefId), sellerProductId);

            if (!isSuccess)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        
        [Function("AddVariantProduct")]
        public async Task<HttpResponseData> AddVariantProduct(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "create/{sellerProductId}/variant")]
            HttpRequestData req, int sellerProductId)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _productScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
            
            var request = await pVariantRequestMapper.Map(req.Body);
            
            var isSuccess =  await productService.PostVariantToExistingProduct(Guid.Parse(userRefId), sellerProductId, request);

            if (!isSuccess)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            response.StatusCode = HttpStatusCode.OK;
            return response;
            
        }
        
        [Function("UpdateVariant")]
        public async Task<HttpResponseData> UpdateVariant(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "update/variant")]
            HttpRequestData req)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _productScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
            
            var request = await pVariantRequestMapper.Map(req.Body);
            
            var isSuccess = await productService.UpdateVariant(Guid.Parse(userRefId), request);

            if (!isSuccess)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            response.StatusCode = HttpStatusCode.OK;
            return response;
            
        }
        
        [Function("DeleteVariant")]
        public async Task<HttpResponseData> DeleteVariant(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "delete/variant/{variantId}")]
            HttpRequestData req, int variantId)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _productScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
            
            var isSuccess = await productService.DeleteVariant(Guid.Parse(userRefId), variantId);

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