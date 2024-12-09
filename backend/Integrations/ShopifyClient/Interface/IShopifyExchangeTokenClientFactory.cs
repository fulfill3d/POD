namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyExchangeTokenClientFactory
    {
        IShopifyExchangeTokenClient CreateClient(string shop);
    }
}