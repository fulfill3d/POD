using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using POD.API.Admin.Model.Data.Models;
using POD.API.Admin.Model.Services.Interfaces;
using POD.API.Admin.Model.Services.Options;
using POD.Integrations.BlobClient.Interface;
using POD.Integrations.BlobClient.Model;

namespace POD.API.Admin.Model.Services
{
    public class ModelBlobService(
        IOptions<ModelOptions> threedmodelServiceOption,
        IBlobClient blobClient,
        ILogger<ModelService> logger
    ): IModelBlobService
    {

        public async Task<BlobClientResponse> UploadBlobAsync(UploadModelFileRequest fileRequest)
        {
            var type = Common.Core.Parse.File.GetContentType(fileRequest.ContentType);
            var extension = Common.Core.Parse.File.GetExtension(fileRequest.ContentType);
            var uniqueName = $"{Guid.NewGuid().ToString()}{extension}";
                    
            var blob = new Blob
            {
                Container = threedmodelServiceOption.Value.BlobContainerName,
                Name = uniqueName,
                Bytes = fileRequest.Content,
                Type = type
            };
            
            return new BlobClientResponse
            {
                BlobUrl = await blobClient.Upload(blob),
                BlobName = uniqueName
            };
        }
        
        public async Task<Stream> DownloadBlobAsync(string blobName)
        {
            var blob = new Blob
            {
                Container = threedmodelServiceOption.Value.BlobContainerName,
                Name = blobName,
            };
            return await blobClient.Download(blob);
        }

        public async Task DeleteBlobAsync(string blobName)
        {
            var blob = new Blob
            {
                Container = threedmodelServiceOption.Value.BlobContainerName,
                Name = blobName,
            };
            await blobClient.Delete(blob);
        }

        public async Task DeleteBlobListAsync(List<string> blobList)
        {
            
            await Task.WhenAll(blobList.Select<string, Task>(async blobName =>
            {
                var blob = new Blob
                {
                    Container = threedmodelServiceOption.Value.BlobContainerName,
                    Name = blobName,
                };
                await blobClient.Delete(blob);
            }));
        }
        
    }
}