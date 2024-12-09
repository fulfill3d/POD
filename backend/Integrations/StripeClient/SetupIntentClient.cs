using AutoMapper;
using Microsoft.Extensions.Options;
using POD.Integrations.StripeClient.Interface;
using POD.Integrations.StripeClient.Model.SetupIntent;
using POD.Integrations.StripeClient.Options;
using Stripe;

namespace POD.Integrations.StripeClient
{
    internal class SetupIntentClient(IOptions<StripeClientOptions> options, IMapper mapper) : BaseClient(options), ISetupIntentClient
    {
        public SetupIntentResult SetupCardIntent(string stripeCustomerId)
        {
            var intentOptions = new SetupIntentCreateOptions
            {
                Customer = stripeCustomerId,
                PaymentMethodTypes = { "card" },
            };
            var intentService = new SetupIntentService();
            var intent = intentService.Create(intentOptions);
            return mapper.Map<SetupIntent, SetupIntentResult>(intent);
        }
    }
}