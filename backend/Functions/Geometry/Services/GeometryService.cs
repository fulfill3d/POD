using Microsoft.EntityFrameworkCore;
using POD.Functions.Geometry.Data.Database;
using POD.Common.Utils.ThreeDimGeometry;
using POD.Functions.Geometry.Services.Interfaces;
using BoundingBox = POD.Common.Utils.ThreeDimGeometry.BoundingBox;

namespace POD.Functions.Geometry.Services
{
    public class GeometryService(GeometryContext dbContext) : IGeometryService
    {
        public async Task DetermineGeomSpec(Stream blob, string blobName)
        {
            using var memoryStream = new MemoryStream();
            
            await blob.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();
            
            BoundingBox bb;
            double volume;
                
            if (StlParser.IsAsciiStl(fileBytes))
            {
                bb = StlParser.CalculateBoundingBoxFromAscii(fileBytes);
                volume = StlParser.CalculateVolumeFromAscii(fileBytes);
            }
            else
            {
                bb = StlParser.CalculateBoundingBoxFromBinary(fileBytes);
                volume = StlParser.CalculateVolumeFromBinary(fileBytes);
            }

            var modelFile = await dbContext.ModelFiles.FirstOrDefaultAsync(mf =>
                mf.IsEnabled == true && mf.BlobName == blobName);
                
            if (modelFile == null) return; // TODO Email developer
                
            modelFile.Volume = (decimal)volume;
            modelFile.IsVolumeDetermined = true;
                
            modelFile.BoundX = Math.Abs((decimal)(bb.MaxX - bb.MinX));
            modelFile.BoundY = Math.Abs((decimal)(bb.MaxY - bb.MinY));
            modelFile.BoundZ = Math.Abs((decimal)(bb.MaxZ - bb.MinZ));
            modelFile.IsBoundingBoxDetermined = true;

            await dbContext.SaveChangesAsync();
        }
    }
}