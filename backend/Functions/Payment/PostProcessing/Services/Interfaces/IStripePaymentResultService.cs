using POD.Functions.Payment.Common.Data.Models;

namespace POD.Functions.Payment.PostProcessing.Services.Interfaces

{
    public interface IStripePaymentResultService
    {
        Task SaveStripePaymentResult(PaymentResultMessage<StripeResultDetails> paymentResult);
    }
}
