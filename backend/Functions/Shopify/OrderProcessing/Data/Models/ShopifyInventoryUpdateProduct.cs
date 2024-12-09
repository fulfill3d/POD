namespace POD.Functions.Shopify.OrderProcessing.Data.Models
{
    public class ShopifyInventoryUpdateProduct
    {
        public long ProductOnlineStoreId { get; set; }
        public int StoreId { get; set; }
        public string Shop { get; set; }
        public string Token { get; set; }
    }
}
