using Microsoft.Extensions.DependencyInjection;
using POD.Common.Database;
using POD.Common.Service;
using POD.Functions.Shopify.CreateWebhooks.Data.Database;
using POD.Functions.Shopify.CreateWebhooks.Services;
using POD.Functions.Shopify.CreateWebhooks.Services.Interfaces;
using POD.Functions.Shopify.CreateWebhooks.Services.Options;
using POD.Integrations.ShopifyClient;

namespace POD.Functions.Shopify.CreateWebhooks
{
    public static class DepInj
    {
        public static void RegisterServices(
            this IServiceCollection services,
            DbConnectionOptions dbConnectionsOptions,
            Action<CreateWebhooksServiceOption> configureCreateWebhooks)
        {
            #region Miscellaneous

            services.ConfigureServiceOptions<CreateWebhooksServiceOption>((_, opt) => configureCreateWebhooks(opt));
            services.AddDatabaseContext<CreateWebhooksContext>(dbConnectionsOptions);

            #endregion

            #region Services
            
            services.AddTransient<ICreateWebhooksService, CreateWebhooksService>();
            services.AddTransient<IStoreService, StoreService>();
            
            services.AddShopifyWebhooksClient();
            services.AddShopifyFullfilmentServiceClient();
            services.AddShopifyAccessScopeClient();

            #endregion
        }
    }
}