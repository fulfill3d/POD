using POD.Integrations.ServiceBusClient.Interfaces;
using POD.Integrations.ServiceBusClient.Options;
using Microsoft.Extensions.DependencyInjection;
using POD.Common.Service;

namespace POD.Integrations.ServiceBusClient
{
    public static class DepInj
    {
        public static void RegisterServiceBusClient(
            this IServiceCollection services,
            Action<ServiceBusClientOptions> configureServiceBusClientOptions)
        {
            services.ConfigureServiceOptions<ServiceBusClientOptions>((_, options) => configureServiceBusClientOptions(options));
            services.AddTransient<IServiceBusClient, ServiceBusClient>();
        }
    }
}