
namespace POD.Integrations.ShopifyClient.Interface
{
    public interface IShopifyInventoryItemClientFactory
    {
        IShopifyInventoryItemClient CreateClient(string shop, string token);
    }
}
