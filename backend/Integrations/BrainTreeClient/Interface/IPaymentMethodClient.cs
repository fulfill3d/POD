using POD.Integrations.BrainTreeClient.Model.PaymentMethod;

namespace POD.Integrations.BrainTreeClient.Interface
{
    public interface IPaymentMethodClient
    {
        Task<PaymentMethodResult> CreatePaymentMethod(string sellerId, string nonce);
    }
}