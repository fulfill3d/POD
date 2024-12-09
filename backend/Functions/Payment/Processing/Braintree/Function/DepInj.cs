using Microsoft.Extensions.DependencyInjection;
using POD.Common.Service;
using POD.Functions.Payment.Processing.Braintree.Services;
using POD.Functions.Payment.Processing.Braintree.Services.Interfaces;
using POD.Functions.Payment.Processing.Braintree.Services.Options;
using POD.Integrations.BrainTreeClient;
using POD.Integrations.BrainTreeClient.Options;
using POD.Integrations.ServiceBusClient;
using POD.Integrations.ServiceBusClient.Options;

namespace POD.Functions.Payment.Processing.Braintree
{
    public static class DepInj
    {
        public static void RegisterServices(
            this IServiceCollection services,
            Action<ServiceBusClientOptions> serviceOptions,
            Action<QueueNames> queueNames,
            Action<BraintreeClientOptions> configureBraintreeClientOptions)
        {
            #region Options

            services.ConfigureServiceOptions<QueueNames>((_, options) => queueNames(options));

            #endregion

            #region Services
            
            services.AddBraintreeTransactionClients(configureBraintreeClientOptions);
            services.RegisterServiceBusClient(serviceOptions);
            services.AddTransient<IBraintreeService, BraintreeService>();
            services.AddTransient<IServiceBusService, ServiceBusService>();

            #endregion
        }
    }
}