using POD.Functions.Shopify.OrderProcessing.Data.Models.Shopify;

namespace POD.Functions.Shopify.OrderProcessing.Data.Models
{
    public class ShopifyNewOrderMessage
    {
        public string Shop { get; set; }
        public Order Order { get; set; }
    }
}
