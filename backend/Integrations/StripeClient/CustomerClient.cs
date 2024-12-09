using AutoMapper;
using Microsoft.Extensions.Options;
using POD.Integrations.StripeClient.Interface;
using POD.Integrations.StripeClient.Model.Customer;
using POD.Integrations.StripeClient.Options;
using Stripe;

namespace POD.Integrations.StripeClient
{
    internal class CustomerClient(IOptions<StripeClientOptions> options, IMapper mapper) : BaseClient(options), ICustomerClient
    {
        public CustomerResult CreateCustomer(CreateCustomerRequest request)
        {
            var options = new CustomerCreateOptions
            {
                Email = request.Email,
                Phone = request.Phone,
                Name = request.Name
            };
            var customerService = new CustomerService();
            var customer = customerService.Create(options);
            return mapper.Map<Customer, CustomerResult>(customer);
        }
    }
}