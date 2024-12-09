namespace POD.Functions.Shopify.CreateWebhooks.Services.Options
{
    public class CreateWebhooksServiceOption
    {
        public string ShopifyWebhookEndpointsBaseUrl { get; set; }
        public string PodShopifyInventoryTrackingUpdatesCallbackUrl { get; set; }
        public string PodFullfilmentHandleName { get; set; }
        public string PodFullfilmentServiceName { get; set; }
        public string ShopifyWriteInventoryScopeName { get; set; }
    }
}