using AutoMapper;
using POD.Integrations.StripeClient.Model.Customer;
using POD.Integrations.StripeClient.Model.SetupIntent;
using Stripe;

namespace POD.Integrations.StripeClient.Options
{
    public class SetupPaymentProfile : Profile
    {
        public SetupPaymentProfile()
        {
            CreateMap<SetupIntent, SetupIntentResult>().ReverseMap();
            CreateMap<Customer, CustomerResult>();
        }
    }
}