
using POD.Functions.Shopify.CreateWebhooks.Data.Models;

namespace POD.Functions.Shopify.CreateWebhooks.Services.Interfaces
{
    public interface ICreateWebhooksService
    {
        Task<bool> RegisterWebhooks(CreateWebhookMessage request);
    }
}