using Microsoft.Extensions.DependencyInjection;
using POD.Common.Database;
using POD.Common.Service;
using POD.Functions.Shopify.WebhookEndpoints.Data.Database;
using POD.Functions.Shopify.WebhookEndpoints.Data.Models;
using POD.Functions.Shopify.WebhookEndpoints.Service.Options;
using POD.Functions.Shopify.WebhookEndpoints.Services;
using POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces;
using POD.Integrations.BlobClient;
using POD.Integrations.BlobClient.Options;
using POD.Integrations.ServiceBusClient;
using POD.Integrations.ServiceBusClient.Options;
using POD.Integrations.ShopifyClient;

namespace POD.Functions.Shopify.WebhookEndpoints
{
    public static class DepInj
    {
        public static void RegisterServices(
            this IServiceCollection services,
            DbConnectionOptions dbConnectionsOptions,
            Action<ServiceBusClientOptions> serviceBusClientOptions,
            Action<BlobClientConfiguration> configureBlobClientOptions,
            Action<ShopifyApiEndpointsOptions> configureWebhookEndpointsService,
            Action<TokenOptions> tokenConfiguration,
            Action<RequestTimeout> requestTimeout,
            Action<B2COptions> b2COptions)
        {
            #region Options

            services.ConfigureServiceOptions<ShopifyApiEndpointsOptions>((_, options) => configureWebhookEndpointsService(options));
            services.ConfigureServiceOptions<TokenOptions>((_, options) => tokenConfiguration(options));
            services.ConfigureServiceOptions<RequestTimeout>((_, options) => requestTimeout(options));
            services.ConfigureServiceOptions<B2COptions>((_, options) => b2COptions(options));

            #endregion

            #region Services
            
            services.AddDatabaseContext<WebhookEndpointsContext>(dbConnectionsOptions);
            services.RegisterServiceBusClient(serviceBusClientOptions);
            services.RegisterBlobClient(configureBlobClientOptions);
            
            services.AddShopifyAuthorizationClient();
            services.AddShopifyShopClient();
            services.AddShopifyRequest();

            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IBlobService, BlobService>();
            services.AddTransient<IInstallService, InstallService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IServiceBusService, ServiceBusService>();
            services.AddTransient<IStoreService, StoreService>();
            services.AddTransient<IUninstallService, UninstallService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<ISellerService, SellerService>();
            services.AddTransient<IShopifyService, ShopifyService>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<IUserService, UserService>();
            
            #endregion
        }
    }
}