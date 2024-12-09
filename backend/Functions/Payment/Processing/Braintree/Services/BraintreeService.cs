using Microsoft.Extensions.Logging;
using POD.Functions.Payment.Common.Data.Models;
using POD.Functions.Payment.Processing.Braintree.Services.Interfaces;
using POD.Integrations.BrainTreeClient.Interface;

namespace POD.Functions.Payment.Processing.Braintree.Services
{
    public class BraintreeService(
        ITransactionClient transactionClient, 
        ILogger<BraintreeService> logger, 
        IServiceBusService serviceBusService) : IBraintreeService
    {
        public async Task ChargeBraintree(ChargePaymentMessage<BraintreeDetails> chargePaymentMessage)
        {
            PaymentResultMessage<BraintreeResultDetails> paymentResultMessage = new PaymentResultMessage<BraintreeResultDetails>
            {
                SellerId = chargePaymentMessage.SellerId,
                StoreId = chargePaymentMessage.StoreId,
                SellerPaymentMethodId = chargePaymentMessage.SellerPaymentMethodId,
                PaymentTransaction = chargePaymentMessage.PaymentTransaction,
            };

            try
            {
                var transactionResult = await transactionClient.CreateTransaction(
                    chargePaymentMessage.TotalCost,
                    chargePaymentMessage.PaymentDetails.PaymentMethodNonce,
                    chargePaymentMessage.PaymentDetails.DeviceData,
                    chargePaymentMessage.Currency
                );

                paymentResultMessage.IsSucceeded = true;
                paymentResultMessage.PaymentResultDetails = new BraintreeResultDetails
                {
                    TransactionId = transactionResult.Id
                };

                logger.LogInformation($"{chargePaymentMessage.TotalCost} successfully charged from customer with " +
                                      $"BraintreeCustomerId:{chargePaymentMessage.PaymentDetails.BraintreeSellerId}");
               
            }
            catch (Exception ex)
            {
                paymentResultMessage.IsSucceeded = false;
                paymentResultMessage.ErrorMessage = ex.Message;

                logger.LogError($"{chargePaymentMessage.TotalCost} couldn't be charged from customer with " +
                                $"BraintreeCustomerId:{chargePaymentMessage.PaymentDetails.BraintreeSellerId}");
            }


            await serviceBusService.SendBraintreePaymentResultMessage(paymentResultMessage);
        }
    }
}