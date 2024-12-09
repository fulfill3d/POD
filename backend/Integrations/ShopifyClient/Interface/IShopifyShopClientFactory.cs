namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyShopClientFactory
    {
        IShopifyShopClient CreateClient(string shop, string token);
    }
}
