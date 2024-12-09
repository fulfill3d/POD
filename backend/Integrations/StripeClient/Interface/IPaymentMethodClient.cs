using POD.Integrations.StripeClient.Model.PaymentMethod;

namespace POD.Integrations.StripeClient.Interface
{
    public interface IPaymentMethodClient
    {
        PaymentMethodResult GetPaymentMethod(string id);
        void Detach(string id);
    }
}