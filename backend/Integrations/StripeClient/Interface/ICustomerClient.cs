using POD.Integrations.StripeClient.Model.Customer;

namespace POD.Integrations.StripeClient.Interface
{
    public interface ICustomerClient
    {
        CustomerResult CreateCustomer(CreateCustomerRequest request);
    }
}