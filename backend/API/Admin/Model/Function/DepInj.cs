using Microsoft.Extensions.DependencyInjection;
using POD.API.Admin.Model.Data.Database;
using POD.API.Admin.Model.Services;
using POD.API.Admin.Model.Services.Interfaces;
using POD.API.Admin.Model.Services.Options;
using POD.Common.Core.Model;
using POD.Common.Database;
using POD.Common.Service;
using POD.Integrations.BlobClient;
using POD.Integrations.BlobClient.Options;

namespace POD.API.Admin.Model
{
    public static class DepInj
    {
        public static void RegisterServices(
            this IServiceCollection services,
            DbConnectionOptions dbConnectionsOptions,
            Action<BlobClientConfiguration> configureBlobClient,
            Action<TokenValidationOptions> tokenValidationOptions,
            Action<AuthorizationScope> authorizationScope,
            Action<ModelOptions> configureThreeDModel)
        {
            services.AddHttpRequestBodyMapper();
            services.ConfigureServiceOptions<ModelOptions>((_, options) => configureThreeDModel(options));
            services.ConfigureServiceOptions<AuthorizationScope>((_, options) => authorizationScope(options));
            services.AddB2CJwtTokenValidator((_, options) => tokenValidationOptions(options));
            services.AddDatabaseContext<ModelContext>(dbConnectionsOptions);
            services.RegisterBlobClient(configureBlobClient);
            services.AddTransient<IModelService, ModelService>();
            services.AddTransient<IModelBlobService, ModelBlobService>();
        }
    }
}