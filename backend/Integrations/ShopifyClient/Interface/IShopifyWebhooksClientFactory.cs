namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyWebhooksClientFactory
    {
        public IShopifyWebhooksClient CreateClient(string shop, string token);
    }
}
