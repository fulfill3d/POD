using Microsoft.Extensions.DependencyInjection;
using POD.Common.Service;
using POD.Functions.Shopify.CallExecutes.Services;
using POD.Functions.Shopify.CallExecutes.Services.Interfaces;
using POD.Functions.Shopify.CallExecutes.Services.Options;
using POD.Integrations.ServiceBusClient;
using POD.Integrations.ServiceBusClient.Options;
using POD.Integrations.ShopifyClient;

namespace POD.Functions.Shopify.CallExecutes
{
    public static class DepInj
    {
        public static void RegisterServices(
            this IServiceCollection services,
            Action<ServiceBusClientOptions> configureServiceBus,
            Action<LocationConfig> locationConfig,
            Action<QueueNames> queueNames,
            Action<UpdateInventoryOptions> updateOptions)
        {
            #region Options

            services.ConfigureServiceOptions<LocationConfig>((_, options) => locationConfig(options));
            services.ConfigureServiceOptions<QueueNames>((_, options) => queueNames(options));
            services.ConfigureServiceOptions<UpdateInventoryOptions>((_, options) => updateOptions(options));

            #endregion

            #region Services
            
            services.RegisterServiceBusClient(configureServiceBus);
            
            services.AddShopifyProductClient();
            services.AddShopifyLocationClient();
            services.AddShopifyFulfillmentRequestClient();
            services.AddShopifyFulfillmentOrderClient();
            services.AddShopifyOrderFulfillmentClient();
            services.AddShopifyInventoryLevelClient();
            services.AddShopifyInventoryItemClient();

            services.AddTransient<IShopifyTaskService, ShopifyTaskService>();
            services.AddTransient<IShopifyProductService, ShopifyProductService>();
            services.AddTransient<IShopifyFulfillmentService, ShopifyFulfillmentService>();
            services.AddTransient<IShopifyFulfillmentOrderService, ShopifyFulfillmentOrderService>();
            services.AddTransient<IShopifyLocationService, ShopifyLocationService>();
            services.AddTransient<IServiceBusService, ServiceBusService>();
            services.AddTransient<IShopifyUpdateInventoryService, ShopifyUpdateInventoryService>();

            #endregion
        }
    }
}