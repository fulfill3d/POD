using POD.Functions.Shopify.WebhookEndpoints.Data.Models;

namespace POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces
{
    public interface IInstallService
    {
        Task<ServiceResult<string>> Install(string store);
    }
}
