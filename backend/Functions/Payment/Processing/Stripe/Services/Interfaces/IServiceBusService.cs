using POD.Functions.Payment.Common.Data.Models;

namespace POD.Functions.Payment.Processing.Stripe.Services.Interfaces
{
    public interface IServiceBusService
    {
        Task SendStripePaymentResultMessage(PaymentResultMessage<StripeResultDetails> paymentResultMessage);
    }
}
