using POD.Functions.Shopify.WebhookEndpoints.Data.Models;

namespace POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces
{
    public interface IServiceBusService
    {
        Task SendOrderCreateMessageToServiceBus(ShopOrder shopOrder, string sessionId, CancellationToken cancellationToken);
        Task SendInstallMessageToServiceBus(NewInstallQueueMessage message);
        Task SendUninstallMessageToServiceBus(string message, CancellationToken cancellationToken);
        Task SendUpdateShopMessageToServiceBus(string message, CancellationToken cancellationToken);
        Task SendDeleteProductMessageToServiceBus(DeleteProduct deleteProduct, CancellationToken cancellationToken);
        Task SendDeleteOrderMessageToServiceBus(ShopOrder shopOrder, CancellationToken cancellationToken);
        Task SendCreateWebhookMessageToServiceBus(CreateWebhook createWebhook);
    }
}
