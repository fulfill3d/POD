namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyProductVariantClientFactory
    {
        IShopifyProductVariantClient CreateClient(string shop, string token);
    }
}
