namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyLocationClientFactory
    {
        IShopifyLocationClient CreateClient(string shop, string token);
    }
}
