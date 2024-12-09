using POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces;

namespace POD.Functions.Shopify.WebhookEndpoints.Services
{
    public class UninstallService(IServiceBusService serviceBusService) : IUninstallService
    {
        public async Task Uninstall(string message, CancellationToken cancellationToken)
        {
            await serviceBusService.SendUninstallMessageToServiceBus(message, cancellationToken);
        }
    }
}