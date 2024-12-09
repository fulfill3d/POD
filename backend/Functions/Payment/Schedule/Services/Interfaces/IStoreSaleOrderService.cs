using POD.Common.Database.Models;

namespace POD.Functions.Payment.Schedule.Services.Interfaces
{
    public interface IStoreSaleOrderService
    {
        IQueryable<StoreSaleOrder> GetByOrderStatus(IList<int> orderStatusList, int? processCount = null);
        Task ChangeCustomerSaleOrdersStatusesAsPaymentErrorAsync(IEnumerable<StoreSaleOrder> storeSaleOrders);
        Task ChangeCustomerSaleOrdersStatusesAsProcessingPayment(IEnumerable<StoreSaleOrder> storeSaleOrders);
    }
}
