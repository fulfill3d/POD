using Microsoft.Extensions.Logging;
using POD.API.Seller.Common.Services.Interfaces;
using POD.Functions.Shopify.AppInstall.Data.Models;
using POD.Functions.Shopify.AppInstall.Services.Interfaces;

namespace POD.Functions.Shopify.AppInstall.Services
{
    public class AppInstallService(
        ICommonSellerService commonSellerService,
        IEmailService emailService,
        ILogger<AppInstallService> logger
    )
        : IAppInstallService
    {
        public async Task<bool> SendWelcomeMailToSeller(ShopifyInstallMessage request)
        {
            // request.Shop ??= string.Empty;
            //
            // var seller = await _commonSellerService.GetByShop(request.Shop);
            // if (seller == null)
            // {
            //     _logger.LogError($"No customer found with shop: {request.Shop}");
            //     return false;
            // }
            // TODO SendGrid | Implement/Commentout after SendGrid
            await emailService.SendWelcomeMailAsync(request.Name, request.Email);

            return true;
        }
    }
}