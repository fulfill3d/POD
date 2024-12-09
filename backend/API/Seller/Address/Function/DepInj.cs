using Microsoft.Extensions.DependencyInjection;
using POD.API.Seller.Address.Data.Database;
using POD.API.Seller.Address.Services;
using POD.API.Seller.Address.Services.Interfaces;
using POD.API.Seller.Common.Services;
using POD.Common.Core.Model;
using POD.Common.Database;
using POD.Common.Service;

namespace POD.API.Seller.Address
{
    public static class DepInj
    {
        public static void RegisterServices(
            this IServiceCollection services,
            DbConnectionOptions dbConnectionsOptions,
            Action<TokenValidationOptions> tokenValidationOptions,
            Action<AuthorizationScope> authorizationScope)
        {
            #region Options
            services.ConfigureServiceOptions<AuthorizationScope>((_, options) => authorizationScope(options));
            #endregion
            #region Services

            services.AddDatabaseContext<AddressContext>(dbConnectionsOptions);
            services.AddB2CJwtTokenValidator((_, options) => tokenValidationOptions(options));
            services.AddCommonSellerServices(dbConnectionsOptions);
            services.AddHttpRequestBodyMapper();
            
            services.AddTransient<IAddressService, AddressService>();

            #endregion
            
            #region Services
            services.AddFluentValidation();
            services.AddFluentValidator<Data.Models.Address>();
            #endregion
        }
    }
}