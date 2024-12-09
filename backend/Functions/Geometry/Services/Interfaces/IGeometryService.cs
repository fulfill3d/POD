namespace POD.Functions.Geometry.Services.Interfaces
{
    public interface IGeometryService
    {
        Task DetermineGeomSpec(Stream blob, string blobName);
    }
}