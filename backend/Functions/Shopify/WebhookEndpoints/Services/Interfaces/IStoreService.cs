using POD.Common.Database.Models;

namespace POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces
{
    public interface IStoreService
    {
        Task NewOrExistingStore(User user, int sellerId, string storeName, string code);
        Task UpdateShop(string message, CancellationToken cancellationToken);
    }
}
