using Braintree;
using Microsoft.Extensions.Options;
using POD.Integrations.BrainTreeClient.Options;

namespace POD.Integrations.BrainTreeClient
{
    internal abstract class BaseClient(IOptions<BraintreeClientOptions> options)
    {
        protected BraintreeGateway Gateway { get; } = new(
            options.Value.UseSandbox ? Braintree.Environment.SANDBOX : Braintree.Environment.PRODUCTION,
            options.Value.MerchantId,
            options.Value.PublicKey,
            options.Value.PrivateKey
        );
    }
}