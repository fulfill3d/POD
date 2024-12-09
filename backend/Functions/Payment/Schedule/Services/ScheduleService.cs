using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using POD.Common.Database.Models;
using POD.Functions.Payment.Common.Data.Models;
using POD.Functions.Payment.Schedule.Services.Interfaces;
using POD.Functions.Payment.Schedule.Services.Options;

namespace POD.Functions.Payment.Schedule.Services
{
    public class ScheduleService(
        IOptions<PaymentProcessingOptions> options,
        IStoreSaleOrderService storeSaleOrderService,
        IStoreSaleTransactionService storeSaleTransactionService,
        IPaymentProviderService paymentProviderService,
        IEmailService emailService,
        IServiceBusService serviceBusService
    ) : IScheduleService
    {
        private readonly int _capturePaymentRetryCount = options.Value.CapturePaymentRetryCount;
        
        public async Task TriggerPaymentProcessing()
        {
            var orderStatusFilter = new List<int>()
            {
                (int)POD.Common.Core.Enum.OrderStatus.PaymentError, 
                (int)POD.Common.Core.Enum.OrderStatus.Received,
                (int)POD.Common.Core.Enum.OrderStatus.TestOrder // TODO REMOVE THAT
            };

            var allStoreOrders = await storeSaleOrderService
                .GetByOrderStatus(orderStatusFilter, _capturePaymentRetryCount)
                .ToListAsync();

            var groupedStoreOrders = allStoreOrders
                .GroupBy(o => o.Store)
                .ToDictionary(o => o.Key, o => o.ToList());

            foreach (var storeOrderKeyValuePair in groupedStoreOrders)
            {
                await ProcessPayments(storeOrderKeyValuePair.Key, storeOrderKeyValuePair.Value);
            }
        }

        private async Task ProcessPayments(Store store, IEnumerable<StoreSaleOrder> storeSaleOrders)
        {
            decimal totalCost = 0m;

            var defaultPaymentMethod = store.Seller.SellerPaymentMethods
                .FirstOrDefault(t => t.IsDefault && t.IsEnabled);

            if (defaultPaymentMethod == null)
            {
                await emailService.NotifyStorePaymentError(store);
                await storeSaleOrderService.ChangeCustomerSaleOrdersStatusesAsPaymentErrorAsync(storeSaleOrders);
                return;
            }

            var paymentTransaction = new PaymentTransaction()
            {
                SellerPaymentMethodId = defaultPaymentMethod.Id,
                IsEnabled = true,
                LastModifiedDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow
            };

            foreach (var order in storeSaleOrders)
            {
                var storeSaleTransaction = storeSaleTransactionService.CreateStoreSaleTransactionOfStoreSaleOrder(order);

                totalCost += storeSaleTransaction.Total;
                paymentTransaction.SaleTransactions.Add(storeSaleTransaction);
            }
            
            if (totalCost <= 0) return;
            
            paymentTransaction.Total = totalCost;

            var chargePaymentMessage = await paymentProviderService.CreateChargePaymentMessage(store.Id, defaultPaymentMethod.Id, totalCost, paymentTransaction);
            
            await serviceBusService.SendChargePaymentMessage(chargePaymentMessage);

            await storeSaleOrderService.ChangeCustomerSaleOrdersStatusesAsProcessingPayment(storeSaleOrders);
        }
    }
}