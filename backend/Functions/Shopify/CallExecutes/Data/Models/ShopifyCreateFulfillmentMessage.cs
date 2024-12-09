namespace POD.Functions.Shopify.CallExecutes.Data.Models
{
    public class ShopifyCreateFulfillmentMessage
    {
        public string Shop { get; set; }
        public string Token { get; set; }
        public long ShopifyOrderId { get; set; }
        public IEnumerable<ShopifyOrderLineItem> ShopifyOrderLineItems { get; set; }
        public TrackingInformation TrackingInformation { get; set; }
        public bool NotifyEndCustomer { get; set; }
        public int PodOrderId { get; set; }
        public long LocationId { get; set; }
    }
}
