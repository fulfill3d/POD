using Microsoft.Extensions.DependencyInjection;
using POD.Common.Service;
using POD.Functions.Payment.Processing.Stripe.Services;
using POD.Functions.Payment.Processing.Stripe.Services.Interfaces;
using POD.Functions.Payment.Processing.Stripe.Services.Options;
using POD.Integrations.ServiceBusClient;
using POD.Integrations.ServiceBusClient.Options;
using POD.Integrations.StripeClient;
using POD.Integrations.StripeClient.Options;

namespace POD.Functions.Payment.Processing.Stripe
{
    public static class DepInj
    {
        public static void RegisterServices(
            this IServiceCollection services,
            Action<ServiceBusClientOptions> serviceOptions,
            Action<QueueNames> queueNames,
            Action<StripeClientOptions> configureStripeClientOptions)
        {
            #region Options

            services.ConfigureServiceOptions<QueueNames>((_, options) => queueNames(options));

            #endregion

            #region Services
            
            services.RegisterServiceBusClient(serviceOptions);
            services.AddStripeSetupPaymentClients(configureStripeClientOptions);
            services.AddTransient<IStripeService, StripeService>();
            services.AddTransient<IServiceBusService, ServiceBusService>();

            #endregion
        }
    }
}