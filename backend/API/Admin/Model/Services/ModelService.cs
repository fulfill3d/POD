using System.IO.Compression;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POD.API.Admin.Model.Data.Database;
using POD.API.Admin.Model.Data.Models;
using POD.API.Admin.Model.Services.Interfaces;
using POD.Common.Core.Enum;
using POD.Common.Database.Models;
using POD.Common.Utils.Extensions;

namespace POD.API.Admin.Model.Services
{
    public class ModelService(
        ModelContext dbContext,
        IModelBlobService blobService,
        ILogger<ModelService> logger
    ) : IModelService
    {
        public async Task<bool> UploadModel(UploadModelRequest request)
        {

            var uploadTasks = request.Files.Select(async file =>
            {
                var response = await blobService.UploadBlobAsync(file);

                if (string.IsNullOrEmpty(response.BlobUrl))
                    throw new Exception("The requested file does not exist");

                return new ModelFile
                {
                    Name = file.FileName.DiscardFileExtension(),
                    BlobName = response.BlobName,
                    Type = file.ContentType,
                    Uri = response.BlobUrl,
                    Size = file.Size,
                    CreatedAt = DateTime.Now,
                    IsVolumeDetermined = false,
                    IsBoundingBoxDetermined = false,
                    IsEnabled = false
                };
            }).ToArray();

            var modelFiles = await Task.WhenAll(uploadTasks);
            
            var model = new POD.Common.Database.Models.Model
            {
                IsEnabled = true,
                Name = request.Name,
                Summary = request.Description,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                ModelCategoryId = request.CategoryId,
                ModelFiles = modelFiles.ToList()
            };
                
            dbContext.Models.Add(model);
                
            await dbContext.SaveChangesAsync();
                
            return true;
        }

        public async Task<DownloadFileModel?> DownloadModel(int modelId)
        {
            var modelNameTask = dbContext.Models
                .Where(m => m.Id == modelId)
                .Select(m => m.Name)
                .FirstOrDefaultAsync();
            
            var blobNamesTask = dbContext.ModelFiles
                .Where(f => f.ThreeDmodelId == modelId && !string.IsNullOrEmpty(f.BlobName))
                .Select(f => f.BlobName)                                     
                .ToListAsync();
            
            await Task.WhenAll(modelNameTask, blobNamesTask);

            var name = modelNameTask.Result;
            var blobNames = blobNamesTask.Result;

            if (blobNames.Count == 0)
            {
                return null;
            }

            var fileName = GenerateZipNameForModelOrFile(name);
            
            Stream contentStream = await DownloadAndCompressBlobListAsync(fileName, blobNames);

            return new DownloadFileModel
            {
                Content = contentStream,
                ContentType = ContentType.ZIP,
                Name = fileName
            };
        }

        public async Task<bool> DeleteModel(int modelId)
        {
            try
            {
                var model = await dbContext.Models
                    .Include(m => m.ModelFiles)
                    .FirstOrDefaultAsync(m => m.IsEnabled == true && m.Id == modelId);

                if (model == null)
                {
                    return false;
                }

                var blobList = await dbContext.ModelFiles
                    .Where(f => f.IsEnabled == true && f.ThreeDmodelId == modelId)
                    .Select(f => f.BlobName)
                    .ToListAsync();
                
                model.IsEnabled = false;
                
                foreach (var modelFile in model.ModelFiles)
                {
                    modelFile.IsEnabled = false;
                }
                
                await blobService.DeleteBlobListAsync(blobList);
                
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                dbContext.RevertAllChangesInTheContext();
                
                return false;
            }
        }

        public async Task<bool> UpdateModel(int modelId, UpdateModelRequest request)
        {
            try
            {
                var model = await dbContext.Models
                    .FirstOrDefaultAsync(m => m.IsEnabled == true && m.Id == modelId);

                if (model == null)
                {
                    return false;
                }
                
                model.ModelCategoryId = request.CategoryId;
                model.Name = request.Name;
                model.Summary = request.Description;
                model.UpdatedAt = DateTime.Now;
            
                await dbContext.SaveChangesAsync();
            
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> UploadModelFileToExistingModel(int modelId, List<UploadModelFileRequest> fileRequest)
        {
            var uploadTasks = fileRequest.Select(async file =>
            {
                BlobClientResponse response = await blobService.UploadBlobAsync(file);

                if (string.IsNullOrEmpty(response.BlobUrl))
                    throw new Exception("The requested file does not exist");

                return new ModelFile
                {
                    ThreeDmodelId = modelId,
                    Name = file.FileName.DiscardFileExtension(),
                    BlobName = response.BlobName,
                    Type = file.ContentType,
                    Uri = response.BlobUrl,
                    Size = file.Size,
                    CreatedAt = DateTime.Now,
                    IsVolumeDetermined = false,
                    IsBoundingBoxDetermined = false,
                    IsEnabled = false
                };
            }).ToArray();

            var modelFiles = await Task.WhenAll(uploadTasks);
            
            await dbContext.ModelFiles.AddRangeAsync(modelFiles);
            
            await dbContext.SaveChangesAsync();

            return true;
        }
        
        public async Task<DownloadFileModel?> DownloadModelFile(int fileId)
        {
            try
            {
                var model = await dbContext.ModelFiles
                    .Where(f => f.IsEnabled == true && f.Id == fileId)
                    .Select(f => new
                    {
                        ModelFileName = f.Name,
                        BlobName = f.BlobName
                    })
                    .FirstOrDefaultAsync();
            
                if (model == null)
                {
                    return null;
                }
            
                var fileName = GenerateZipNameForModelOrFile(model.ModelFileName);
                Stream contentStream = await DownloadAndCompressBlobAsync(model.BlobName);

                return new DownloadFileModel
                {
                    Content = contentStream,
                    ContentType = ContentType.ZIP,
                    Name = fileName
                };
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> DeleteModelFile(int fileId)
        {
            try
            {
                var modelFile = await dbContext.ModelFiles
                    .FirstOrDefaultAsync(f => f.IsEnabled == true && f.Id == fileId);

                if (modelFile == null)
                {
                    return false;
                }
                
                modelFile.IsEnabled = false;

                var blobList = await dbContext.ModelFiles
                    .Where(f => f.IsEnabled == true && f.Id == fileId)
                    .Select(f => f.BlobName)
                    .ToListAsync();

                await blobService.DeleteBlobListAsync(blobList);
                
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> UpdateModelFile(int fileId, UpdateModelFileRequest request)
        {
            try
            {
                var modelFile = await dbContext.ModelFiles
                    .FirstOrDefaultAsync(f => f.IsEnabled == true && f.Id == fileId);

                if (modelFile == null)
                {
                    return false;
                }
                
                modelFile.Name = request.FileName;
            
                await dbContext.SaveChangesAsync();
            
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
        // Private methods
        private async Task<MemoryStream> DownloadAndCompressBlobListAsync(string fileName, List<string> blobNames)
        {
            var compressedFileStream = new MemoryStream();
            using (var archive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, true))
            {
                foreach (var blobName in blobNames)
                {
                    var blobStream = await blobService.DownloadBlobAsync(blobName);
                    var zipEntry = archive.CreateEntry(fileName, CompressionLevel.Optimal);

                    await using (var entryStream = zipEntry.Open())
                    {
                        await blobStream.CopyToAsync(entryStream);
                    }
                    blobStream.Close();
                }
            }
            
            logger.LogInformation($"compressedFileStream.Position: {compressedFileStream.Position}");

            compressedFileStream.Position = 0; // Reset the memory stream position for reading
            return compressedFileStream;
        }
        
        private async Task<MemoryStream> DownloadAndCompressBlobAsync(string blobName)
        {
            var compressedFileStream = new MemoryStream();
            using (var archive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, true))
            {
                
                var blobStream = await blobService.DownloadBlobAsync(blobName);
                var zipEntry = archive.CreateEntry(blobName);

                await using (var entryStream = zipEntry.Open())
                {
                    await blobStream.CopyToAsync(entryStream);
                }
                blobStream.Close();
            }

            compressedFileStream.Position = 0;
            return compressedFileStream;
        }

        private string GenerateZipNameForModelOrFile(string? name)
        {
            if (name == null)
            {
                // If modelName is null, generate a random string
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                Random random = new Random();
                string randomString = new string(Enumerable.Repeat(chars, 10)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
                return randomString + ".zip";
            }
            // If modelName is not null, format it
            name = name.ToLower(); // Lowercase all characters
            name = name.Replace(' ', '-'); // Replace spaces with '-'
            // Replace any other characters with '-'
            name = Regex.Replace(name, "[^a-zA-Z0-9-]", "-");
            return name + ".zip";
        }
    }
}