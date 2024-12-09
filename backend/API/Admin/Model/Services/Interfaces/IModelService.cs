using POD.API.Admin.Model.Data.Models;

namespace POD.API.Admin.Model.Services.Interfaces
{
    public interface IModelService
    {
        Task<bool> UploadModel(UploadModelRequest request);
        Task<DownloadFileModel?> DownloadModel(int modelId);
        Task<bool> DeleteModel(int modelId);
        Task<bool> UpdateModel(int modelId, UpdateModelRequest request);
        Task<bool> UploadModelFileToExistingModel(int modelId, List<UploadModelFileRequest> fileRequest);
        Task<DownloadFileModel?> DownloadModelFile(int fileId);
        Task<bool> DeleteModelFile(int fileId);
        Task<bool> UpdateModelFile(int fileId, UpdateModelFileRequest request);
    }
}