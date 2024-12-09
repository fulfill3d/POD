namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyOrderClientFactory
    {
        IShopifyOrderClient CreateClient(string shop, string token);
    }
}
