using Microsoft.Extensions.Logging;
using POD.Common.Core.Enum;
using POD.Common.Service.Interfaces;
using POD.Functions.Payment.Common.Data.Models;
using POD.Functions.Payment.PostProcessing.Services.Interfaces;

namespace POD.Functions.Payment.PostProcessing.Services
{
	public class BraintreePaymentResultService(
        ILogger<BraintreePaymentResultService> logger,
        ICommonStoreSaleOrderService commonStoreSaleOrderService,
        ISellerPaymentService sellerPaymentService) : IBraintreePaymentResultService
	{
        public async Task SaveBraintreePaymentResult(PaymentResultMessage<BraintreeResultDetails> paymentResult)
        {
            var storeSaleOrderIds = paymentResult.PaymentTransaction.SaleTransactions.Select(t => t.StoreSaleOrderId).ToList();

            if (!paymentResult.IsSucceeded)
            {
                foreach (var storeSaleOrderId in storeSaleOrderIds)
                {
                    await commonStoreSaleOrderService.ChangeStoreSaleOrderStatus(storeSaleOrderId, OrderStatus.PaymentError);
                }
                
                logger.LogError($"Braintree payment failed to capture with customer id {paymentResult.StoreId}!\n" +
                    $"Error message: {paymentResult.ErrorMessage}");
                return;
            }
            
            foreach (var storeSaleOrderId in storeSaleOrderIds)
            {
                await commonStoreSaleOrderService.ChangeStoreSaleOrderStatus(storeSaleOrderId, OrderStatus.PaymentCleared);
            }
            
            await sellerPaymentService.SaveBraintreePaymentTransaction(paymentResult);

            logger.LogInformation(
                $"Braintree transacation saved with id {paymentResult.PaymentResultDetails.TransactionId} " +
                $"for customer {paymentResult.StoreId}");
        }
    }
}