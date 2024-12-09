namespace POD.Functions.Shopify.UpdateInventory.Data.Models
{
    public class UpdateInventoryServiceMessage
    {
        public long ProductOnlineStoreId { get; set; }
        public string Shop { get; set; }
        public string Token { get; set; }
    }
}