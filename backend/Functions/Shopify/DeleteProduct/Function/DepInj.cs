using Microsoft.Extensions.DependencyInjection;
using POD.Common.Database;
using POD.Common.Service;
using POD.Functions.Shopify.DeleteProduct.Services;
using POD.Functions.Shopify.DeleteProduct.Services.Interfaces;

namespace POD.Functions.Shopify.DeleteProduct
{
    public static class DepInj
    {
        public static void RegisterServices(
            this IServiceCollection services,
            DbConnectionOptions dbOption)
        {
            #region Services

            services.AddTransient<IDeleteProductService, DeleteProductService>();
            services.AddCommonStoreProductService(dbOption);

            #endregion
        }
    }
}