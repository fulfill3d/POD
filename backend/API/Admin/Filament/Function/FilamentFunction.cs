using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using POD.API.Admin.Filament.Data.Models;
using POD.API.Admin.Filament.Services.Interfaces;
using POD.Common.Core.Model;
using POD.Common.Service.Interfaces;

namespace POD.API.Admin.Filament
{
    public class FilamentFunction(
        IFilamentService filamentService,
        IJwtValidatorService jwtService,
        IOptions<AuthorizationScope> opt,
        IHttpRequestBodyMapper<AddFilamentRequest> aFRequestBodyMapper,
        IHttpRequestBodyMapper<UpdateFilamentStockRequest> uFsRequestBodyMapper)
    {
        
        private readonly AuthorizationScope _filamentScope = opt.Value;
        
        [Function("GetFilaments")]
        public async Task<HttpResponseData> GetFilaments(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "get-filaments")]
            HttpRequestData req)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _filamentScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }

            var result = await filamentService.GetAllFilaments();

            await response.WriteStringAsync(result);
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        
        [Function("GetFilament")]
        public async Task<HttpResponseData> GetFilament(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "get-filament/{filamentId}")]
            HttpRequestData req, int filamentId)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _filamentScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }

            var result = await filamentService.GetFilament(filamentId);

            await response.WriteStringAsync(result);
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        
        [Function("AddFilament")]
        public async Task<HttpResponseData> AddFilament(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "add-filament")]
            HttpRequestData req)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _filamentScope.Write };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }

            var request = await aFRequestBodyMapper.Map(req.Body);
            var isSuccess = await filamentService.AddNewFilament(request);

            if (!isSuccess)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        
        [Function("UpdateStock")]
        public async Task<HttpResponseData> UpdateStock(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "update-filament-stock")]
            HttpRequestData req)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _filamentScope.Write };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }

            var request = await uFsRequestBodyMapper.Map(req.Body);
            var isSuccess = await filamentService.UpdateFilamentStock(request);

            if (!isSuccess)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        
        [Function("DeleteFilament")]
        public async Task<HttpResponseData> DeleteFilament(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "delete-filament/{filamentId}")]
            HttpRequestData req, 
            int filamentId)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _filamentScope.Write };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }

            var isSuccess = await filamentService.DeleteFilament(filamentId);

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