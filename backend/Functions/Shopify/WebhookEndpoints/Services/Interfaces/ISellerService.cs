namespace POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces
{
    public interface ISellerService
    {
        Task<int> NewOrExistingSeller(POD.Common.Database.Models.User user);
    }
}
