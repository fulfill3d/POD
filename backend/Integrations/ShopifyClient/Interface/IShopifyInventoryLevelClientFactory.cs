namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyInventoryLevelClientFactory
    {
        IShopifyInventoryLevelClient CreateClient(string shop, string token);

    }
}
