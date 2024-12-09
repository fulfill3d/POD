using POD.Functions.Shopify.WebhookEndpoints.Data.Models;

namespace POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces
{
    public interface IUserService
    {
        Task<POD.Common.Database.Models.User> NewOrExistingUser(B2CUser user);
    }
}