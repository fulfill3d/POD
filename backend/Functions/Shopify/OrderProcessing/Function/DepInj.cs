using Microsoft.Extensions.DependencyInjection;
using POD.Common.Database;
using POD.Common.Service;
using POD.Functions.Shopify.OrderProcessing.Data.Database;
using POD.Functions.Shopify.OrderProcessing.Services;
using POD.Functions.Shopify.OrderProcessing.Services.Interfaces;
using POD.Functions.Shopify.OrderProcessing.Services.Options;
using POD.Integrations.ServiceBusClient;
using POD.Integrations.ServiceBusClient.Options;

namespace POD.Functions.Shopify.OrderProcessing
{
    public static class DepInj
    {
        public static void RegisterServices(
            this IServiceCollection services,
            DbConnectionOptions dbConnectionsOptions,
            Action<ServiceBusClientOptions> serviceBusClientOptions,
            Action<ServiceBusQueueNames> serviceQueueNames)
        {
            #region Miscellaneous

            services.ConfigureServiceOptions<ServiceBusQueueNames>((_, opt) => serviceQueueNames(opt));
            services.AddDatabaseContext<OrderProcessingContext>(dbConnectionsOptions);

            #endregion

            #region Services
            
            services.AddCommonStoreProductService(dbConnectionsOptions);
            services.RegisterServiceBusClient(serviceBusClientOptions);
            
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IOrderDetailNumberGenerator, OrderDetailNumberGenerator>();
            services.AddTransient<IOrderNumberGenerator, OrderNumberGenerator>();
            services.AddTransient<IServiceBusService, ServiceBusService>();
            services.AddTransient<IShopifyOrderService, ShopifyOrderService>();
            services.AddTransient<IStoreOrderService, StoreOrderService>();
            services.AddTransient<IStoreService, StoreService>();

            #endregion
        }
    }
}