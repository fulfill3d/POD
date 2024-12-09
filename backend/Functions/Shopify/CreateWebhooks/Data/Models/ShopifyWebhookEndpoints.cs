namespace POD.Functions.Shopify.CreateWebhooks.Data.Models
{
    public class ShopifyWebhookEndpoints(string HostName)
    {
        public string UninstallUrl => $"https://{HostName}/api/webhooks/app/uninstall";

        public string OrderCreatedUrl => $"https://{HostName}/api/webhooks/order/create";

        public string OrderDeletedUrl => $"https://{HostName}/api/webhooks/order/delete";

        public string DeleteProductsUrl => $"https://{HostName}/api/webhooks/product/delete";

        public string ShopUpdateUrl => $"https://{HostName}/api/webhooks/shop/update";
    }
}