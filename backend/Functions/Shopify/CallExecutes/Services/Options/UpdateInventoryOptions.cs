namespace POD.Functions.Shopify.CallExecutes.Services.Options
{
    public class UpdateInventoryOptions
    {
        public bool ShopifyUpdateInventoryForAllVariants { get; set; }
        public string PodVendorName { get; set; }
        public string PodFulfillmentServiceName { get; set; }
        public int DefaultInventoryLevel { get; set; }
        public int InventoryUpdateLimit { get; set; }
    }
}
