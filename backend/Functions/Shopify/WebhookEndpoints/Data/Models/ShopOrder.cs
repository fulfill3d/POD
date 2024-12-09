namespace POD.Functions.Shopify.WebhookEndpoints.Data.Models
{
    public class ShopOrder
    {
        public string Shop { get; set; }
        public POD.Integrations.ShopifyClient.Model.Order.Order Order { get; set; }
    }
}
