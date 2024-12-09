using AutoMapper;
using POD.Integrations.StripeClient.Model.PaymentIntent;
using Stripe;

namespace POD.Integrations.StripeClient.Options
{
    public class PaymentIntentProfile : Profile
    {
        public PaymentIntentProfile()
        {
            CreateMap<PaymentIntent, PaymentIntentResult>().ReverseMap();
        }
    }
}