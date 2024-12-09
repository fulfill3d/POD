using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using POD.Functions.Geometry.Services.Interfaces;

namespace POD.Functions.Geometry
{
    public class GeometryFunction(IGeometryService geometryService)
    {
        [Function("determine-geometric-specification")]
        public async Task Run(
            [BlobTrigger("devcontainer/{name}", Connection = "BlobConnectionString")]
            Stream blob,
            string name,
            ILogger log)
        {
            log.LogInformation("Determine Geometric Spec processing started.");
            await geometryService.DetermineGeomSpec(blob, name);
        }
    }
}