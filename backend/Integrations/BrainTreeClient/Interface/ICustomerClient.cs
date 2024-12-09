using POD.Integrations.BrainTreeClient.Model.Customer;

namespace POD.Integrations.BrainTreeClient.Interface
{
    public interface ICustomerClient
    {
        Task<CustomerResult> CreateCustomer(CreateCustomerRequest request);
        Task<GenerateClientTokenResult> GenerateClientToken(string customerId);
    }
}