using Microsoft.Extensions.Logging;
using POD.Functions.Payment.Common.Data.Models;
using POD.Functions.Payment.Processing.Stripe.Services.Interfaces;
using POD.Integrations.StripeClient.Interface;
using POD.Integrations.StripeClient.Model.PaymentIntent;

namespace POD.Functions.Payment.Processing.Stripe.Services
{
    public class StripeService(
        IPaymentIntentClient paymentIntentClient,
        ILogger<StripeService> logger,
        IServiceBusService serviceBusService) : IStripeService
    {
        public async Task ChargeStripe(ChargePaymentMessage<StripeDetails> chargePaymentMessage)
        {
            PaymentResultMessage<StripeResultDetails> paymentResultMessage = new PaymentResultMessage<StripeResultDetails>
            {
                SellerId = chargePaymentMessage.SellerId,
                SellerPaymentMethodId = chargePaymentMessage.SellerPaymentMethodId,
                PaymentTransaction = chargePaymentMessage.PaymentTransaction,
            };

            try
            {
                PaymentIntentResult paymentIntentResult = await paymentIntentClient.ChargeStripe(
                    amount: (long)(chargePaymentMessage.TotalCost * 100),
                    currency: chargePaymentMessage.Currency,
                    customerId: chargePaymentMessage.PaymentDetails.StripeSellerId,
                    paymentMethodId: chargePaymentMessage.PaymentDetails.StripePaymentId);

                if (paymentIntentResult.Status.Equals("succeeded"))
                {
                    paymentResultMessage.IsSucceeded = true;
                    paymentResultMessage.PaymentResultDetails = new StripeResultDetails
                    {
                        PaymentIntentId = paymentIntentResult.Id
                    };

                    logger.LogInformation($"{chargePaymentMessage.TotalCost} successfully charged from customer with " +
                        $"StripeCustomerId:{chargePaymentMessage.PaymentDetails.StripeSellerId}" +
                        $"StripePaymentId:{chargePaymentMessage.PaymentDetails.StripePaymentId}");
                }
                else
                {
                    paymentResultMessage.IsSucceeded = false;
                    paymentResultMessage.ErrorMessage = paymentIntentResult.Status;

                    logger.LogError($"{chargePaymentMessage.TotalCost} couldn't be charged from customer with " +
                        $"StripeCustomerId:{chargePaymentMessage.PaymentDetails.StripeSellerId}" +
                        $"StripePaymentId:{chargePaymentMessage.PaymentDetails.StripePaymentId}");
                }
            }
            catch (Exception ex)
            {
                paymentResultMessage.IsSucceeded = false;
                paymentResultMessage.ErrorMessage = ex.Message;

                logger.LogError($"{chargePaymentMessage.TotalCost} couldn't be charged from customer with " +
                    $"StripeCustomerId:{chargePaymentMessage.PaymentDetails.StripeSellerId}" +
                    $"StripePaymentId:{chargePaymentMessage.PaymentDetails.StripePaymentId}");
            }


            await serviceBusService.SendStripePaymentResultMessage(paymentResultMessage);
        }
    }
}