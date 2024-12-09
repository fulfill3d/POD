namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyAccessScopeClientFactory
    {
        public IShopifyAccessScopeClient CreateClient(string shop, string token);
    }
}
