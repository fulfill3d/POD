using POD.Functions.Payment.Common.Data.Models;

namespace POD.Functions.Payment.PostProcessing.Services.Interfaces
{
    public interface IBraintreePaymentResultService
	{
        Task SaveBraintreePaymentResult(PaymentResultMessage<BraintreeResultDetails> paymentResult);
    }
}