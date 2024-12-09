using Microsoft.Extensions.DependencyInjection;
using POD.API.Admin.Filament.Data.Database;
using POD.API.Admin.Filament.Services;
using POD.API.Admin.Filament.Services.Interfaces;
using POD.Common.Core.Model;
using POD.Common.Database;
using POD.Common.Service;

namespace POD.API.Admin.Filament
{
    public static class DepInj
    {
        public static void AddFilamentServices(
            this IServiceCollection services,
            DbConnectionOptions dbOption,
            Action<TokenValidationOptions> tokenValidationOptions,
            Action<AuthorizationScope> authorizationScope)
        {
            services.ConfigureServiceOptions<AuthorizationScope>((_, options) => authorizationScope(options));
            services.AddB2CJwtTokenValidator((_, options) => tokenValidationOptions(options));
            services.AddHttpRequestBodyMapper();
            services.AddDatabaseContext<FilamentContext>(dbOption);
            services.AddTransient<IFilamentService, FilamentService>();
        }
    }
}