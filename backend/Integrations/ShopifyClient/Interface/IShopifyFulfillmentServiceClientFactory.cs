namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyFulfillmentServiceClientFactory
    {
        IShopifyFulfillmentServiceClient CreateClient(string shop, string token);
    }
}
