using POD.Functions.Payment.Common.Data.Models;

namespace POD.Functions.Payment.Processing.Stripe.Services.Interfaces
{
    public interface IStripeService
    {
        Task ChargeStripe(ChargePaymentMessage<StripeDetails> chargePaymentMessage);
    }
}