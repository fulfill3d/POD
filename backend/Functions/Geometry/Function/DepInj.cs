using Microsoft.Extensions.DependencyInjection;
using POD.Functions.Geometry.Data.Database;
using POD.Functions.Geometry.Services;
using POD.Functions.Geometry.Services.Interfaces;
using POD.Common.Database;
using POD.Common.Service;

namespace POD.Functions.Geometry
{
    public static class DepInj
    {
        public static void RegisterServices(
            this IServiceCollection services,
            DbConnectionOptions dbConnectionsOptions)
        {
            services.AddDatabaseContext<GeometryContext>(dbConnectionsOptions);
            services.AddTransient<IGeometryService, GeometryService>();
        }
    }
}