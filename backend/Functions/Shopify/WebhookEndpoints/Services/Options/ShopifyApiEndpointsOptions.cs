namespace POD.Functions.Shopify.WebhookEndpoints.Service.Options
{
    public class ShopifyApiEndpointsOptions
    {
        public string ShopifyAPIKey { get; set; }
        public string ShopifyWebhookEndpointsBaseUrl { get; set; }
        public string ShopifySecretKey { get; set; }
        public string ShopifyCreateWebhooksServiceBusName { get; set; }
        public string ShopifyUninstallServiceBusName { get; set; }
        public string ShopifyOrderProcessingServiceBusName { get; set; }
        public string ShopifyInstallServiceBusName { get; set; }
        public string ShopifyOrdersDeletedServiceBusName { get; set; }
        public string ShopifyStoreStatusUpdateQueueName { get; set; }
        public string ShopifyDeleteProductsServiceBusName { get; set; }
        public string OrderErrorBlobName { get; set; }
        public string PodFrontEndUrl { get; set; }
    }
}
