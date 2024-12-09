using POD.API.Seller.Payment.Data.Models.Braintree;
using POD.Integrations.BrainTreeClient.Model.Customer;

namespace POD.API.Seller.Payment.Services.Interfaces
{
    public interface IBraintreeService
    {
        Task<GenerateClientTokenResult> GetClientToken(Guid userRefId);
        Task<Data.Models.SellerPaymentMethod> CompleteSetup(Guid userId, CompleteSetupRequest request);
    }
}