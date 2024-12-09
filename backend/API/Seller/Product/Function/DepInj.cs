using System.Collections.Specialized;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using POD.API.Common.Core;
using POD.API.Common.Core.Mapper;
using POD.API.Seller.Common.Services;
using POD.API.Seller.Product.Data.Database;
using POD.API.Seller.Product.Services;
using POD.API.Seller.Product.Services.Interfaces;
using POD.Common.Core.Model;
using POD.Common.Database;
using POD.Common.Service;
using POD.Common.Service.Interfaces;

namespace POD.API.Seller.Product
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
            services.AddDatabaseContext<ProductContext>(dbConnectionsOptions);
            services.AddTransient<IProductService, ProductService>();
            services.AddCommonSellerServices(dbConnectionsOptions);
            #endregion

            #region Mappers
            services.AddScoped<IMapper<NameValueCollection, Pagination>, PaginationParametersMapper>();
            #endregion

            #region Validators
            //TODO Check FluentValidatorServiceCollection
            services.AddScoped<IValidator<Pagination>, PaginationParametersValidator>();
            #endregion
        }
    }
}