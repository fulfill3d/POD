using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Options;
using POD.API.Seller.Common.Services.Interfaces;
using POD.API.Seller.Store.Data.Models;
using POD.API.Seller.Store.Services.Interfaces;
using POD.Common.Core.Model;
using POD.Common.Service.Interfaces;

namespace POD.API.Seller.Store
{
    public class StoreFunction(
        ICommonSellerService sellerService,
        IStoreService storeService, 
        IJwtValidatorService jwtService,
        IOptions<AuthorizationScope> opt,
        IHttpRequestBodyMapper<ProductRequest> productRequestBodyMapper)
    {
        
        private readonly AuthorizationScope _storeScope = opt.Value;
        
        [Function("PublishProduct")]
        public async Task<HttpResponseData> PublishProduct(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "seller/store/{storeId}/publish")]
            HttpRequestData req, 
            int storeId)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _storeScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
                
            var sellerId = await sellerService.GetSellerIdByUserId(Guid.Parse(userRefId));
            
            var request = await productRequestBodyMapper.Map(req.Body);
            var isSuccess = await storeService.CreateStoreProductBySellerProduct(sellerId, storeId, request);

            if (!isSuccess)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        
        // TODO Get/Update/Delete store products
    }
}