using Microsoft.Extensions.Options;
using POD.Functions.Shopify.WebhookEndpoints.Data.Models;
using POD.Functions.Shopify.WebhookEndpoints.Service.Options;
using POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces;
using POD.Integrations.ShopifyClient.Model.Shopify;

namespace POD.Functions.Shopify.WebhookEndpoints.Services
{
    public class InstallService(IOptions<ShopifyApiEndpointsOptions> opt) : IInstallService
    {
        private readonly string _shopifyApiKey = opt.Value.ShopifyAPIKey;
        private readonly string _shopifyAppUrl = opt.Value.ShopifyWebhookEndpointsBaseUrl;

        public async Task<ServiceResult<string>> Install(string store)
        {
            var shopifyApiEndpoints = new ShopifyApiEndpoints(store, _shopifyAppUrl, _shopifyApiKey);
            var url = shopifyApiEndpoints.AuthorizeUrl + shopifyApiEndpoints.InventoryScope + shopifyApiEndpoints.RedirectUri;
            return new ServiceResult<string>(url, false, false);
        }
    }
}
