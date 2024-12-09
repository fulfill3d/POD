using Microsoft.Extensions.DependencyInjection;
using POD.API.Seller.Common.Services;
using POD.API.Seller.Payment.Data.Database;
using POD.API.Seller.Payment.Services;
using POD.API.Seller.Payment.Services.Interfaces;
using POD.API.Seller.Payment.Services.Options;
using POD.Common.Core.Model;
using POD.Common.Database;
using POD.Common.Service;
using POD.Integrations.BrainTreeClient;
using POD.Integrations.BrainTreeClient.Options;
using POD.Integrations.StripeClient;
using POD.Integrations.StripeClient.Options;

namespace POD.API.Seller.Payment
{
    public static class DepInj
    {
        public static void RegisterServices(
            this IServiceCollection services,
            DbConnectionOptions dbConnectionsOptions,
            Action<PaymentOptions> configurePayment,
            Action<TokenValidationOptions> tokenValidationOptions,
            Action< AuthorizationScope> authorizationScope,
            Action<BraintreeClientOptions> configureBraintreeClientOptions,
            Action<StripeClientOptions> configureStripeClientOptions)
        {
            #region Options
            services.ConfigureServiceOptions<PaymentOptions>((_, options) => configurePayment(options));
            services.ConfigureServiceOptions<AuthorizationScope>((_, options) => authorizationScope(options));
            #endregion

            #region Services
            services.AddDatabaseContext<PaymentContext>(dbConnectionsOptions);
            services.AddB2CJwtTokenValidator((_, options) => tokenValidationOptions(options));

            services.AddStripeSetupPaymentClients(configureStripeClientOptions);
            services.AddBraintreeSetupPaymentClients(configureBraintreeClientOptions);
            
            services.AddCommonSellerServices(dbConnectionsOptions);
            services.AddCommonUserService(dbConnectionsOptions);
            
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IStripeService, StripeService>();
            services.AddTransient<IBraintreeService, BraintreeService>();

            #endregion
        }
    }
}