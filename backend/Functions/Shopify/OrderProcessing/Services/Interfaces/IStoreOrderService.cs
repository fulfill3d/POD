using POD.Functions.Shopify.OrderProcessing.Data.Models.Shopify;

namespace POD.Functions.Shopify.OrderProcessing.Services.Interfaces
{
    public interface IStoreOrderService
    {
        Task SaveStoreOrder(int storeId, Order order);
    }
}