using POD.API.Seller.Payment.Data.Models.Stripe;
using POD.Integrations.StripeClient.Model.SetupIntent;

namespace POD.API.Seller.Payment.Services.Interfaces
{
    public interface IStripeService
    {
        Task<SetupIntentResult> CreateSetupIntent(Guid userId);
        Task<Data.Models.SellerPaymentMethod> CompleteSetup(Guid userId, CompleteSetupRequest intent);
    }
}