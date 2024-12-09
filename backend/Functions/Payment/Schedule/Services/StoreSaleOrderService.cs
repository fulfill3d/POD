using Microsoft.EntityFrameworkCore;
using POD.Common.Database.Models;
using POD.Common.Service.Interfaces;
using POD.Functions.Payment.Schedule.Data.Database;
using POD.Functions.Payment.Schedule.Services.Interfaces;

namespace POD.Functions.Payment.Schedule.Services
{
    public class StoreSaleOrderService(ScheduleContext dbContext, ICommonStoreSaleOrderService commonStoreSaleOrderService) : IStoreSaleOrderService
    {
        public async Task ChangeCustomerSaleOrdersStatusesAsPaymentErrorAsync(IEnumerable<StoreSaleOrder> customerOrders)
        {
            foreach (var order in customerOrders)
            {
                await commonStoreSaleOrderService.ChangeStoreSaleOrderStatus(order.Id, POD.Common.Core.Enum.OrderStatus.PaymentError);
            }
        }

        public async Task ChangeCustomerSaleOrdersStatusesAsProcessingPayment(IEnumerable<StoreSaleOrder> storeSaleOrders)
        {
            foreach (var order in storeSaleOrders)
            {
                await commonStoreSaleOrderService.ChangeStoreSaleOrderStatus(order.Id, POD.Common.Core.Enum.OrderStatus.ProcessingPayment);
            }
        }
        
        public IQueryable<StoreSaleOrder> GetByOrderStatus(IList<int> orderStatusList, int? processCount = null)
        {
            
                var saleOrders = dbContext.StoreSaleOrderStatuses
                    .Include(csos => csos.StoreSaleOrder)
                        .ThenInclude(cso => cso.StoreSaleOrderDetails)
                    .Include(csos => csos.StoreSaleOrder)
                        .ThenInclude(cso => cso.Store)
                        .ThenInclude(cso=>cso.Seller)
                        .ThenInclude(cso=>cso.SellerPaymentMethods)
                    .Where(status =>
                        status.IsEnabled == true
                        && status.StoreSaleOrder.IsEnabled == true
                        && orderStatusList.Contains(status.OrderStatusId)
                        && (!processCount.HasValue || status.ProcessCount < processCount.Value))
                    .Select(status => status.StoreSaleOrder)
                    .Distinct();

                return saleOrders;
            
        }

     
    }
}
