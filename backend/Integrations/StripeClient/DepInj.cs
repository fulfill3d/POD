using Microsoft.Extensions.DependencyInjection;
using POD.Common.Service;
using POD.Integrations.StripeClient.Interface;
using POD.Integrations.StripeClient.Options;

namespace POD.Integrations.StripeClient
{
    public static class DepInj
    {
        public static void AddStripePaymentIntentClient(
            this IServiceCollection services,
            Action<StripeClientOptions> configureStripeClientOptions)
        {
            services.ConfigureServiceOptions<StripeClientOptions>((_, options) => configureStripeClientOptions(options));
            
            services.AddAutoMapper(typeof(PaymentIntentProfile));

            services.AddTransient<IPaymentIntentClient, PaymentIntentClient>();
        }

        public static void AddStripeSetupPaymentClients(
            this IServiceCollection services,
            Action<StripeClientOptions> configureStripeClientOptions)
        {
            services.ConfigureServiceOptions<StripeClientOptions>((_, options) => configureStripeClientOptions(options));
            
            services.AddAutoMapper(typeof(SetupPaymentProfile));

            services.AddTransient<ICustomerClient, CustomerClient>();
            services.AddTransient<IPaymentMethodClient, PaymentMethodClient>();
            services.AddTransient<ISetupIntentClient, SetupIntentClient>();
        }
    }
}