using Microsoft.Extensions.DependencyInjection;
using POD.API.Seller.Common.Services;
using POD.API.Seller.Store.Data.Database;
using POD.API.Seller.Store.Services;
using POD.API.Seller.Store.Services.Interfaces;
using POD.Common.Core.Model;
using POD.Common.Database;
using POD.Common.Service;

namespace POD.API.Seller.Store
{
    public static class DepInj
    {
        public static void RegisterServices(
            this IServiceCollection services,
            DbConnectionOptions dbConnectionsOptions,
            Action<TokenValidationOptions> tokenValidationOptions,
            Action<AuthorizationScope> authorizationScope)
        {
            #region Helpers
            services.AddHttpRequestBodyMapper();
            #endregion
            #region Options
            services.ConfigureServiceOptions<AuthorizationScope>((_, options) => authorizationScope(options));
            #endregion
            #region Services
            services.AddB2CJwtTokenValidator((_, options) => tokenValidationOptions(options));
            services.AddCommonSellerServices(dbConnectionsOptions);
            services.AddDatabaseContext<StoreContext>(dbConnectionsOptions);
            services.AddTransient<IStoreService, StoreService>();
            #endregion
        }
    }
}