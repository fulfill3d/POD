using POD.Functions.Payment.Common.Data.Models;

namespace POD.Functions.Payment.Schedule.Services.Interfaces
{
    public interface IServiceBusService
    {
        Task SendChargePaymentMessage(ChargePaymentMessage<PaymentDetails> chargePaymentMessage);
    }
}
