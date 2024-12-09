using AutoMapper;
using POD.Common.Database.Models;
using POD.Functions.Payment.Common.Data.Models;
using POD.Functions.Payment.Schedule.Services.Interfaces;

namespace POD.Functions.Payment.Schedule.Services
{
    public class StoreSaleTransactionService(IMapper mapper) : IStoreSaleTransactionService
    {
        public SaleTransaction CreateStoreSaleTransactionOfStoreSaleOrder(StoreSaleOrder order)
        {
            var storeSaleTransaction = new SaleTransaction()
            {
                StoreSaleOrderId = order.Id,
                IsEnabled = true,
                LastModifiedDate = DateTime.UtcNow,
            };

            var totalByOrder = decimal.Zero;
            var indexByOrder = 0;

            //Product items needs to be shown in descending order by product price
            var sortedOrderDetails = order.StoreSaleOrderDetails
                .OrderByDescending(o => o.Total);

            foreach (var orderDetail in sortedOrderDetails)
            {
                ++indexByOrder;

                var saleTransactionDetail = new SaleTransactionDetail()
                {
                    Order = indexByOrder,
                    IsEnabled = true,
                    LastModifiedDate = DateTime.UtcNow,
                };

                mapper.Map(orderDetail, saleTransactionDetail);

                storeSaleTransaction.SaleTransactionDetails.Add(saleTransactionDetail);

                totalByOrder += orderDetail.Total;
            }

            storeSaleTransaction.Total = totalByOrder;
            return storeSaleTransaction;
        }

    }
}
