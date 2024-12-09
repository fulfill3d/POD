using AutoMapper;
using Microsoft.Extensions.Options;
using POD.Integrations.StripeClient.Interface;
using POD.Integrations.StripeClient.Model.PaymentIntent;
using POD.Integrations.StripeClient.Options;
using Stripe;

namespace POD.Integrations.StripeClient
{
    internal class PaymentIntentClient(IOptions<StripeClientOptions> options, IMapper mapper) : BaseClient(options), IPaymentIntentClient
    {
        public async Task<PaymentIntentResult> ChargeStripe(long amount, string currency, string customerId, string paymentMethodId)
        {
            var service = new PaymentIntentService();
            var options = new PaymentIntentCreateOptions
            {
                Amount = amount,
                Currency = currency,
                Customer = customerId,
                PaymentMethod = paymentMethodId,
                Confirm = true,
                OffSession = true,
            };
            
            var paymentIntent = await service.CreateAsync(options);

            return mapper.Map<PaymentIntentResult>(paymentIntent);
        }
    }
}