namespace POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces
{
    public interface IUninstallService
    {
        Task Uninstall(string message, CancellationToken cancellationToken);
    }
}
