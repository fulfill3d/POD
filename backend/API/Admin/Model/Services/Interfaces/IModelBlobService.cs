using POD.API.Admin.Model.Data.Models;

namespace POD.API.Admin.Model.Services.Interfaces
{
    public interface IModelBlobService
    {
        Task<BlobClientResponse> UploadBlobAsync(UploadModelFileRequest fileRequest);
        Task<Stream> DownloadBlobAsync(string blobName);
        Task DeleteBlobAsync(string blobName);
        Task DeleteBlobListAsync(List<string> blobList);
    }
}