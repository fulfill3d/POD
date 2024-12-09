using POD.Functions.Shopify.CallExecutes.Data.Models;
using POD.Integrations.ShopifyClient.Model;

namespace POD.Functions.Shopify.CallExecutes.Services.Interfaces
{
    public interface IShopifyLocationService
    {
        Task<bool> GetLocationIdForOrderFulfillment(ShopifyGetLocationIdForOrderFulfillmentMessage shopifyGetLocationIdMessage);
        Task<Location?> GetPodLocationAsync(string shop, string token);
    }
}
