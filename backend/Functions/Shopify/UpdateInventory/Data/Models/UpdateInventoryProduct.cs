namespace POD.Functions.Shopify.UpdateInventory.Data.Models
{
    public class UpdateInventoryProduct
    {
        public long ProductOnlineStoreId { get; set; }
        public int StoreId { get; set; }
        public string Shop { get; set; }
        public string Token { get; set; }
    }
}