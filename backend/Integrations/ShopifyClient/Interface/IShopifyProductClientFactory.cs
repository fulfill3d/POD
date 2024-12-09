namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyProductClientFactory
    {
        public IShopifyProductClient CreateClient(string shop, string token);
    }
}
