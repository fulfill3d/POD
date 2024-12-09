using AutoMapper;
using Microsoft.Extensions.Options;
using POD.Integrations.StripeClient.Interface;
using POD.Integrations.StripeClient.Model.PaymentMethod;
using POD.Integrations.StripeClient.Options;
using Stripe;

namespace POD.Integrations.StripeClient
{
    internal class PaymentMethodClient(IOptions<StripeClientOptions> options, IMapper mapper) : BaseClient(options), IPaymentMethodClient
    {
        public PaymentMethodResult GetPaymentMethod(string id)
        {
            var paymentMethodService = new PaymentMethodService();

            var paymentMethod = paymentMethodService.Get(id);
            return new PaymentMethodResult
            {
                PaymentMethodId = paymentMethod.Id,
                Card = new Model.PaymentMethod.Card
                {
                    Brand = paymentMethod.Card.Brand,
                    Country = paymentMethod.Card.Country,
                    Last4 = paymentMethod.Card.Last4,
                    Name = paymentMethod.BillingDetails.Name,
                    ExpireMonth = paymentMethod.Card.ExpMonth.ToString(),
                    ExpireYear = paymentMethod.Card.ExpYear.ToString()
                }
            };
        }

        public void Detach(string id)
        {
            var paymentMethodService = new PaymentMethodService();
            paymentMethodService.Detach(id);
        }
    }
}