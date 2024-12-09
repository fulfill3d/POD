using Microsoft.Extensions.Logging;
using POD.Functions.Shopify.AppUninstall.Services.Interfaces;

namespace POD.Functions.Shopify.AppUninstall.Services
{
    public class AppUninstallService(
        IStoreService storeService,
        ILogger<AppUninstallService> logger,
        IEmailService emailService)
        : IAppUninstallService
    {

        public async Task<bool> ProcessUninstall(string storeName)
        {
            var store = await storeService.Get(storeName);

            if (store == null)
            {
                logger.LogInformation($"No customer found with storeName: {storeName}");
                return false;
            }

            var result = await storeService.Delete(store);
            
            // TODO SendGrid | Implement/Commentout after SendGrid
            // await _emailService.SendUninstallMail(seller.Email, seller.Name);

            return result;
        }
    }
}