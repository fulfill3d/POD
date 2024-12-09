namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyOrderFulfillmentClientFactory
    {
        IShopifyOrderFulfillmentClient CreateClient(string shop, string token);
    }
}
