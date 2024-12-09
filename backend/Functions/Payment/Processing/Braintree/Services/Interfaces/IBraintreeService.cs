using POD.Functions.Payment.Common.Data.Models;

namespace POD.Functions.Payment.Processing.Braintree.Services.Interfaces
{
    public interface IBraintreeService
    {
        Task ChargeBraintree(ChargePaymentMessage<BraintreeDetails> chargePaymentMessage);
    }
}