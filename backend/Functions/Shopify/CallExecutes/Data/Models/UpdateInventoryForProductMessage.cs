namespace POD.Functions.Shopify.CallExecutes.Data.Models
{
    public class UpdateInventoryForProductMessage
    {
        public long ProductOnlineStoreId { get; set; }
        public string Shop { get; set; }
        public string Token { get; set; }
    }
}
