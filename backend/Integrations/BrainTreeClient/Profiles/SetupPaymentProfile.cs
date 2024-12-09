using AutoMapper;
using POD.Integrations.BrainTreeClient.Model.Customer;
using POD.Integrations.BrainTreeClient.Model.PaymentMethod;

namespace POD.Integrations.BrainTreeClient.Profiles
{
    public class SetupPaymentProfile : Profile
    {
        public SetupPaymentProfile()
        {
            CreateMap<CreateCustomerRequest, Braintree.CustomerRequest>();
            CreateMap<Braintree.Customer, CustomerResult>();
            CreateMap<Braintree.PaymentMethod, PaymentMethodResult>();
        }
    }
}