using System.Net;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using POD.API.Admin.Model.Data.Models;
using POD.API.Admin.Model.Services.Interfaces;
using POD.Common.Core.Model;
using POD.Common.Service.Interfaces;

namespace POD.API.Admin.Model
{
    public class ModelFunction(
        IModelService modelService,
        IJwtValidatorService jwtService,
        IOptions<AuthorizationScope> opt,
        IHttpRequestBodyMapper<UpdateModelRequest> updateTdMdRequestBodyMapper, 
        IHttpRequestBodyMapper<UpdateModelFileRequest> updateTdMdFiRequestBodyMapper
    )
    {
        private readonly AuthorizationScope _modelScope = opt.Value;
        
        [Function("UploadModel")]
        public async Task<HttpResponseData> UploadModel(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "upload-model")]
            HttpRequestData req)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _modelScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
            
            if (!req.Headers.Contains("Content-Type") || !req.Headers.GetValues("Content-Type").Any(h => h.Contains("multipart/form-data")))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync("Content-Type must be 'multipart/form-data'.");
                return response;
            }

            var boundary = GetBoundary(req.Headers.GetValues("Content-Type").First());
            if (string.IsNullOrEmpty(boundary))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync("Invalid or missing multipart boundary.");
                return response;
            }
            
            var formData = await ParseMultipartAsync(req.Body, boundary);
            var modelJson = formData.Fields.FirstOrDefault(f => f.Name == "model")?.Value;
            
            if (string.IsNullOrEmpty(modelJson))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync("Model JSON is missing.");
                return response;
            }

            var request = JsonConvert.DeserializeObject<UploadModelRequest>(modelJson);
            
            if (request == null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync("Invalid model data.");
                return response;
            }

            if (formData.Files.Count == 0)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync("Please upload at least one file.");
                return response;
            }

            request.Files = new List<UploadModelFileRequest>();

            request.Files = formData.Files
                .Where(file => file.Content.Length > 0)
                .Select(file => new UploadModelFileRequest
                {
                    FileName = file.FileName,
                    ContentType = "application/octet-stream", // Set a default content type, update as needed
                    Size = file.Content.Length,
                    Content = file.Content
                })
                .ToList();

            var isSuccess = await modelService.UploadModel(request);

            if (!isSuccess)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            response.StatusCode = HttpStatusCode.OK;
            await response.WriteStringAsync("Files uploaded and model processed successfully.");
            return response;
        }
        
        [Function("DownloadModel")]
        public async Task<HttpResponseData> DownloadModel(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "download-model/{modelId}")]
            HttpRequestData req, 
            int modelId)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _modelScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
            
            var fileModel = await modelService.DownloadModel(modelId);
            if (fileModel == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }

            response.StatusCode = HttpStatusCode.OK;
            response.Headers.Add("Content-Disposition", $"attachment; filename={fileModel.Name}");
            response.Headers.Add("Content-Type", fileModel.Type);

            await using var stream = fileModel.Content;
            await stream.CopyToAsync(response.Body);

            return response;
        }

        
        [Function("DeleteModel")]
        public async Task<HttpResponseData> DeleteModel(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "delete-model/{modelId}")]
            HttpRequestData req,
            int modelId)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _modelScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
            
            var isSuccess = await modelService.DeleteModel(modelId);

            if (!isSuccess)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            
            response.StatusCode = HttpStatusCode.OK;
            return response;
            
        }
        
        [Function("UpdateModel")]
        public async Task<HttpResponseData> UpdateModel(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "update-model/{modelId}")]
            HttpRequestData req,
            int modelId)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _modelScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
            
            var request = await updateTdMdRequestBodyMapper.Map(req.Body);
            var isSuccess = await modelService.UpdateModel(modelId, request);

            if (!isSuccess)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        
        [Function("UploadModelFile")]
        public async Task<HttpResponseData> UploadModelFile(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "upload-model-file/{modelId}")]
            HttpRequestData req,
            int modelId)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _modelScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }

            if (!req.Headers.Contains("Content-Type") || !req.Headers.GetValues("Content-Type").Any(h => h.Contains("multipart/form-data")))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync("Content-Type must be 'multipart/form-data'.");
                return response;
            }

            var boundary = GetBoundary(req.Headers.GetValues("Content-Type").First());
            if (string.IsNullOrEmpty(boundary))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync("Invalid or missing multipart boundary.");
                return response;
            }

            var formData = await ParseMultipartAsync(req.Body, boundary);

            if (formData.Files.Count == 0)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync("Please upload at least one file.");
                return response;
            }

            var request = new List<UploadModelFileRequest>(
                from file in formData.Files
                where file.Content.Length > 0
                select new UploadModelFileRequest
                {
                    FileName = file.FileName,
                    ContentType = "application/octet-stream", // Set a default content type, update as needed
                    Size = file.Content.Length,
                    Content = file.Content
                });

            var isSuccess = await modelService.UploadModelFileToExistingModel(modelId, request);

            if (!isSuccess)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync("Failed to upload files to the existing model.");
                return response;
            }

            response.StatusCode = HttpStatusCode.OK;
            await response.WriteStringAsync("Files uploaded to the model successfully.");
            return response;
        }

        
        [Function("DownloadModelFile")]
        public async Task<HttpResponseData> DownloadModelFile(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "download-model-file/{fileId}")]
            HttpRequestData req,
            int fileId)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _modelScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
            
            var fileModel = await modelService.DownloadModelFile(fileId);
            if (fileModel == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }

            response.StatusCode = HttpStatusCode.OK;
            response.Headers.Add("Content-Disposition", $"attachment; filename={fileModel.Name}");
            response.Headers.Add("Content-Type", fileModel.Type);

            await using var stream = fileModel.Content;
            await stream.CopyToAsync(response.Body);

            return response;
        }
        
        [Function("DeleteModelFile")]
        public async Task<HttpResponseData> DeleteModelFile(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "delete-model-file/{fileId}")]
            HttpRequestData req,
            int fileId)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _modelScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
            
            var isSuccess = await modelService.DeleteModelFile(fileId);

            if (!isSuccess)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        
        [Function("UpdateModelFile")]
        public async Task<HttpResponseData> UpdateModelFile(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "update-model-file/{fileId}")]
            HttpRequestData req,
            int fileId)
        {
            var response = req.CreateResponse();
            var acceptedScopes = new[] { _modelScope.Read };
            var userRefId = await jwtService.AuthenticateAndAuthorize(req, acceptedScopes);
            if (userRefId == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
            
            var request = await updateTdMdFiRequestBodyMapper.Map(req.Body);
            var isSuccess = await modelService.UpdateModelFile(fileId, request);

            if (!isSuccess)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        
        

        private static string? GetBoundary(string contentType)
        {
            var elements = contentType.Split(';');
            var boundary = elements.FirstOrDefault(e => e.Trim().StartsWith("boundary", StringComparison.OrdinalIgnoreCase));
            if (boundary != null)
            {
                return boundary.Split('=')[1].Trim('"');
            }
            return null;
        }

        private async Task<MultipartFormData> ParseMultipartAsync(Stream body, string boundary)
        {
            var reader = new MultipartReader(boundary, body);
            var section = await reader.ReadNextSectionAsync();
            var formData = new MultipartFormData();

            while (section != null)
            {
                var contentDisposition = section.GetContentDispositionHeader();

                if (contentDisposition != null)
                {
                    if (contentDisposition.DispositionType.Equals("form-data") && contentDisposition.FileName.HasValue)
                    {
                        var formFile = new FormFile
                        {
                            FileName = contentDisposition.FileName.Value,
                            Content = await ReadStreamToEndAsync(section.Body)
                        };
                        formData.Files.Add(formFile);
                    }
                    else if (contentDisposition.DispositionType.Equals("form-data"))
                    {
                        var formField = new FormField
                        {
                            Name = contentDisposition.Name.Value ?? string.Empty,
                            Value = await new StreamReader(section.Body).ReadToEndAsync()
                        };
                        formData.Fields.Add(formField);
                    }
                }

                section = await reader.ReadNextSectionAsync();
            }

            return formData;
        }

        private static async Task<byte[]> ReadStreamToEndAsync(Stream stream)
        {
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}