using Microsoft.Extensions.Options;
using POD.Functions.Shopify.WebhookEndpoints.Service.Options;
using POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces;
using POD.Integrations.ShopifyClient.Interface;
using POD.Integrations.ShopifyClient.Model;

namespace POD.Functions.Shopify.WebhookEndpoints.Services
{
    public class ShopifyService(
        IShopifyAuthorizationClientFactory shopifyAuthorizationClientFactory,
        IShopifyShopClientFactory shopifyShopClientFactory,
        IOptions<ShopifyApiEndpointsOptions> opt) : IShopifyService
    {
        private readonly string _shopifyApiKey = opt.Value.ShopifyAPIKey;
        private readonly string _shopifySecretKey = opt.Value.ShopifySecretKey;

        public async Task<string> GetAccessToken(string shop, string code)
        {
            var client = shopifyAuthorizationClientFactory.CreateClient(shop);

            var request =
                new ShopifyAccessToken
                {
                    ClientId = _shopifyApiKey,
                    ClientSecret = _shopifySecretKey,
                    Code = code
                };

            var response = await client.GetAccessToken(request);

            return response.Data ?? string.Empty;
        }
        public async Task<ShopifySeller> GetShop(string accessToken, string shop)
        {
            var client = shopifyShopClientFactory.CreateClient(shop, accessToken);

            var response = await client.GetShop();

            return response.Data ?? new ShopifySeller();
        }
    }
}
