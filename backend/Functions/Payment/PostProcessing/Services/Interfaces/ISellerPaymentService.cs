using POD.Functions.Payment.Common.Data.Models;

namespace POD.Functions.Payment.PostProcessing.Services.Interfaces
{
    public interface ISellerPaymentService
    {
        Task SaveBraintreePaymentTransaction(PaymentResultMessage<BraintreeResultDetails> paymentResult);
        Task SaveStripePaymentTransaction(PaymentResultMessage<StripeResultDetails> paymentResult);
        Task SavePayPalPaymentTransaction(PaymentResultMessage<PayPalResultDetails> paymentResult);
    }
}