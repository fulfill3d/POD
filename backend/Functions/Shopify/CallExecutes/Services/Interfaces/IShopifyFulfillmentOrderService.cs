using POD.Integrations.ShopifyClient.Model;

namespace POD.Functions.Shopify.CallExecutes.Services.Interfaces
{
    public interface IShopifyFulfillmentOrderService
    {
        Task<IEnumerable<FulfillmentOrder>> GetPodFulfillmentOrdersAsync(string shop, string token, long shopifyOrderId, long podLocationId);
    }
}
