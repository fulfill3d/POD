using Microsoft.Extensions.DependencyInjection;
using POD.Common.Database;
using POD.Common.Service;
using POD.Functions.Payment.PostProcessing.Data.Database;
using POD.Functions.Payment.PostProcessing.Services;
using POD.Functions.Payment.PostProcessing.Services.Interfaces;

namespace POD.Functions.Payment.PostProcessing
{
    public static class DepInj
    {
        public static void RegisterServices(
            this IServiceCollection services,
            DbConnectionOptions dbConnectionsOptions)
        {
            #region Services
            
            services.AddCommonStoreSaleOrderService(dbConnectionsOptions);
            services.AddDatabaseContext<PostProcessingContext>(dbConnectionsOptions);
            
            services.AddTransient<ISellerPaymentService, SellerPaymentService>();
            services.AddTransient<IBraintreePaymentResultService, BraintreePaymentResultService>();
            services.AddTransient<IStripePaymentResultService, StripePaymentResultService>();

            #endregion
        }
    }
}