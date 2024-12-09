using POD.Integrations.ShopifyClient.Model;

namespace POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces
{
    public interface IShopifyService
    {
        Task<string> GetAccessToken(string shop, string code);
        Task<ShopifySeller> GetShop(string accessToken, string shop);
    }
}
