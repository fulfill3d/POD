using Microsoft.Extensions.DependencyInjection;
using POD.Common.Service;
using POD.Functions.Shopify.UpdateInventory.Services;
using POD.Functions.Shopify.UpdateInventory.Services.Interfaces;
using POD.Functions.Shopify.UpdateInventory.Services.Options;
using POD.Integrations.ServiceBusClient;
using POD.Integrations.ServiceBusClient.Options;

namespace POD.Functions.Shopify.UpdateInventory
{
    public static class DepInj
    {
        public static void RegisterServices(
            this IServiceCollection services,
            Action<ServiceBusClientOptions> configureServiceBusClientOptions,
            Action<UpdateInventoryOptions> updateInventoryOptions)
        {
            #region Miscellaneous

            services.ConfigureServiceOptions<UpdateInventoryOptions>((_, opt) => updateInventoryOptions(opt));

            #endregion

            #region Services
            
            services.RegisterServiceBusClient(configureServiceBusClientOptions);
            services.AddTransient<IServiceBusService, ServiceBusService>();
            services.AddTransient<IUpdateInventoryService, UpdateInventoryService>();

            #endregion
        }
    }
}