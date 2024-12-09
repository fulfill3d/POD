namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyAuthorizationClientFactory
    {
        public IShopifyAuthorizationClient CreateClient(string shop);
    }
}
