using POD.Integrations.StripeClient.Model.SetupIntent;

namespace POD.Integrations.StripeClient.Interface
{
    public interface ISetupIntentClient
    {
        SetupIntentResult SetupCardIntent(string stripeCustomerId);
    }
}