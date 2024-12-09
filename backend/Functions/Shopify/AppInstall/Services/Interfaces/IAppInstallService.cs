using POD.Functions.Shopify.AppInstall.Data.Models;

namespace POD.Functions.Shopify.AppInstall.Services.Interfaces
{
    public interface IAppInstallService
    {
        Task<bool> SendWelcomeMailToSeller(ShopifyInstallMessage request);
    }
}