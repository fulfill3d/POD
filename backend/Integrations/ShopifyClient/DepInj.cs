using Microsoft.Extensions.DependencyInjection;
using POD.Integrations.ShopifyClient.Interface;

namespace POD.Integrations.ShopifyClient
{
    public static class DepInj
    {
        public static void AddShopifyExchangeTokenClient(
            this IServiceCollection services)
        {
            services.AddTransient<IShopifyExchangeTokenClientFactory, ShopifyExchangeTokenClientFactory>();
        }
        
        public static void AddShopifyProductClient(
            this IServiceCollection services)
        {
            services.AddTransient<IShopifyProductClientFactory, ShopifyProductClientFactory>();
        }

        public static void AddShopifyProductVariantClient(
            this IServiceCollection services)
        {
            services.AddTransient<IShopifyProductVariantClientFactory, ShopifyProductVariantClientFactory>();
        }

        public static void AddShopifyWebhooksClient(
            this IServiceCollection services)
        {
            services.AddTransient<IShopifyWebhooksClientFactory, ShopifyWebhooksClientFactory>();
        }

        public static void AddShopifyAccessScopeClient(
            this IServiceCollection services)
        {
            services.AddTransient<IShopifyAccessScopeClientFactory, ShopifyAccessScopeClientFactory>();
        }

        public static void AddShopifyFullfilmentServiceClient(
            this IServiceCollection services)
        {
            services.AddTransient<IShopifyFulfillmentServiceClientFactory, ShopifyFulfillmentServiceClientFactory>();
        }

        public static void AddShopifyInventoryItemClient(
            this IServiceCollection services)
        {
            services.AddTransient<IShopifyInventoryItemClientFactory, ShopifyInventoryItemClientFactory>();
        }

        public static void AddShopifyInventoryLevelClient(
            this IServiceCollection services)
        {
            services.AddTransient<IShopifyInventoryLevelClientFactory, ShopifyInventoryLevelClientFactory>();
        }

        public static void AddShopifyLocationClient(
            this IServiceCollection services)
        {
            services.AddTransient<IShopifyLocationClientFactory, ShopifyLocationClientFactory>();
        }

        public static void AddShopifyOrderFulfillmentClient(
            this IServiceCollection services)
        {
            services.AddTransient<IShopifyOrderFulfillmentClientFactory, ShopifyOrderFulfillmentClientFactory>();
        }

        public static void AddShopifyOrderClient(
            this IServiceCollection services)
        {
            services.AddTransient<IShopifyOrderClientFactory, ShopifyOrderClientFactory>();

        }

        public static void AddShopifyAuthorizationClient(
          this IServiceCollection services)
        {
            services.AddTransient<IShopifyAuthorizationClientFactory, ShopifyAuthorizationClientFactory>();

        }

        public static void AddShopifyShopClient(
        this IServiceCollection services)
        {
            services.AddTransient<IShopifyShopClientFactory, ShopifyShopClientFactory>();

        }

        public static void AddShopifyRequest(
       this IServiceCollection services)
        {
            services.AddTransient<IShopifyRequest, ShopifyRequest>();

        }

        public static void AddShopifyFulfillmentOrderClient(
            this IServiceCollection services)
        {
            services.AddTransient<IShopifyFulfillmentOrderClientFactory, ShopifyFulfillmentOrderClientFactory>();
        }

        public static void AddShopifyFulfillmentRequestClient(
            this IServiceCollection services)
        {
            services.AddTransient<IShopifyFulfillmentRequestClientFactory, ShopifyFulfillmentRequestClientFactory>();
        }
    }
}