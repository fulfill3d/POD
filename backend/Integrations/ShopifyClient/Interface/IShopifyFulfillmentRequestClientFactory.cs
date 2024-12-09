namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyFulfillmentRequestClientFactory
    {
        IShopifyFulfillmentRequestClient CreateClient(string shop, string token);
    }
}
