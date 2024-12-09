using Microsoft.Extensions.DependencyInjection;
using POD.Common.Service;
using POD.Integrations.BrainTreeClient.Interface;
using POD.Integrations.BrainTreeClient.Options;
using POD.Integrations.BrainTreeClient.Profiles;

namespace POD.Integrations.BrainTreeClient
{
    public static class DepInj
    {
        public static void AddBraintreeSetupPaymentClients(
            this IServiceCollection services,
            Action<BraintreeClientOptions> configureBraintreeClientOptions)
        {
            services.ConfigureServiceOptions<BraintreeClientOptions>((_, options) => configureBraintreeClientOptions(options));
            services.AddAutoMapper(typeof(SetupPaymentProfile));
            services.AddTransient<ICustomerClient, CustomerClient>();
            services.AddTransient<IPaymentMethodClient, PaymentMethodClient>();
        }
        
        public static void AddBraintreeTransactionClients(
            this IServiceCollection services,
            Action<BraintreeClientOptions> configureBraintreeClientOptions)
        {
            services.ConfigureServiceOptions<BraintreeClientOptions>((_, options) => configureBraintreeClientOptions(options));
            services.AddAutoMapper(typeof(TransactionProfile));
            services.AddTransient<ITransactionClient, TransactionClient>();
        }
    }
}