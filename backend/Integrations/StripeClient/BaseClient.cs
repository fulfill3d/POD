using Microsoft.Extensions.Options;
using POD.Integrations.StripeClient.Options;
using Stripe;

namespace POD.Integrations.StripeClient
{
    internal abstract class BaseClient
    {
        protected BaseClient(IOptions<StripeClientOptions> options)
        {
            StripeConfiguration.ApiKey = options.Value.ApiKey;
        }
    }
}