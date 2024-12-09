using POD.Integrations.StripeClient.Model.PaymentIntent;

namespace POD.Integrations.StripeClient.Interface
{
    public interface IPaymentIntentClient
    {
        Task<PaymentIntentResult> ChargeStripe(long amount, string currency, string customerId, string paymentMethodId);
    }
}