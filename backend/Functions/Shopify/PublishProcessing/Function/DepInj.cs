using Microsoft.Extensions.DependencyInjection;
using POD.Common.Database;
using POD.Common.Service;
using POD.Functions.Shopify.PublishProcessing.Data.Models;
using POD.Functions.Shopify.PublishProcessing.Data.Database;
using POD.Functions.Shopify.PublishProcessing.Services;
using POD.Functions.Shopify.PublishProcessing.Services.Interfaces;
using POD.Functions.Shopify.PublishProcessing.Services.Options;
using POD.Integrations.ServiceBusClient;
using POD.Integrations.ServiceBusClient.Options;

namespace POD.Functions.Shopify.PublishProcessing
{
    public static class DepInj
    {
        public static void RegisterServices(
            this IServiceCollection services,
            DbConnectionOptions dbConnectionsOptions,
            Action<ServiceBusClientOptions> serviceBusClientOptions,
            Action<QueueNames> configureQueueNames,
            Action<PublishProcessingOptions> publishProcessingOptions)
        {
            #region Miscellaneous

            services.ConfigureServiceOptions<QueueNames>((_, opt) => configureQueueNames(opt));
            services.ConfigureServiceOptions<PublishProcessingOptions>((_, opt) => publishProcessingOptions(opt));
            services.AddDatabaseContext<PublishProcessingContext>(dbConnectionsOptions);

            #endregion

            #region Services

            services.AddCommonStoreProductService(dbConnectionsOptions);
            services.RegisterServiceBusClient(serviceBusClientOptions);
            
            services.AddTransient<IPublishProcessingService, PublishProcessingService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IServiceBusService, ServiceBusService>();

            #endregion
        }
    }
}