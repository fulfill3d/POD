using POD.Functions.Payment.Common.Data.Models;

namespace POD.Functions.Payment.Schedule.Services.Interfaces
{
    public interface IPaymentProviderService
    {
        Task<ChargePaymentMessage<PaymentDetails>> CreateChargePaymentMessage(
            int storeId, 
            int sellerPaymentMethodId, 
            decimal totalCost, 
            PaymentTransaction paymentTransaction);    
    }
}
