namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyFulfillmentOrderClientFactory
    {
        public IShopifyFulfillmentOrderClient CreateClient(string shop, string token);
    }
}
