using POD.Functions.Shopify.OrderProcessing.Data.Models;

namespace POD.Functions.Shopify.OrderProcessing.Services.Interfaces
{
    public interface IShopifyOrderService
    {
        Task<bool> ProcessNewOrder(ShopifyNewOrderMessage message);

    }
}
