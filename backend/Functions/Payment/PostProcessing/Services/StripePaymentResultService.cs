using Microsoft.Extensions.Logging;
using POD.Common.Core.Enum;
using POD.Common.Service.Interfaces;
using POD.Functions.Payment.Common.Data.Models;
using POD.Functions.Payment.PostProcessing.Services.Interfaces;

namespace POD.Functions.Payment.PostProcessing.Services
{
    public class StripePaymentResultService(
        ILogger<StripePaymentResultService> logger,
        ICommonStoreSaleOrderService commonStoreSaleOrderService,
        ISellerPaymentService sellerPaymentService) : IStripePaymentResultService
    {
        public async Task SaveStripePaymentResult(PaymentResultMessage<StripeResultDetails> paymentResult)
        {
            var storeSaleOrderIds = paymentResult.PaymentTransaction.SaleTransactions
                .Select(t => t.StoreSaleOrderId).ToList();

            if (!paymentResult.IsSucceeded)
            {
                foreach (var storeSaleOrderId in storeSaleOrderIds)
                {
                    await commonStoreSaleOrderService.ChangeStoreSaleOrderStatus(storeSaleOrderId, OrderStatus.PaymentError);
                }
                
                logger.LogError($"Stripe payment failed to capture with customer id {paymentResult.StoreId}!\n" +
                                 $"Error message: {paymentResult.ErrorMessage}");
                return;
            }
            
            foreach (var storeSaleOrderId in storeSaleOrderIds)
            {
                await commonStoreSaleOrderService.ChangeStoreSaleOrderStatus(storeSaleOrderId, OrderStatus.PaymentCleared);
            }
            
            await sellerPaymentService.SaveStripePaymentTransaction(paymentResult);

            logger.LogInformation(
                $"Stripe transacation saved with id {paymentResult.PaymentResultDetails.PaymentIntentId} " +
                $"for customer {paymentResult.StoreId}");
        }
    }
}
