namespace POD.Functions.Shopify.CallExecutes.Data.Models
{
    public class ShopifyFulfillOrdersByCustomerMessage
    {
        public int CustomerId { get; set; }
        public long LocationId { get; set; }
        public string Shop { get; set; }
        public string Token { get; set; }
    }
}
