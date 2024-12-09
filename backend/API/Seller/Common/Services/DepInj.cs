using Microsoft.Extensions.DependencyInjection;
using POD.API.Seller.Common.Data;
using POD.API.Seller.Common.Services.Interfaces;
using POD.Common.Database;
using POD.Common.Service;

namespace POD.API.Seller.Common.Services
{
    public static class DepInj
    {
        public static void AddCommonSellerServices(this IServiceCollection services, 
            DbConnectionOptions dbConnectionsOptions)
        {
            services.AddTransient<ICommonSellerService, CommonSellerService>();
            services.AddDatabaseContext<SellerCommonContext>(dbConnectionsOptions);
        }
    }
}