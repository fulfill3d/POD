using Microsoft.Extensions.Options;
using POD.Common.Core.Model;
using POD.Common.Service.Interfaces;
using POD.Functions.Shopify.AppInstall.Service.Options;
using POD.Functions.Shopify.AppInstall.Services.Interfaces;

namespace POD.Functions.Shopify.AppInstall.Services
{
    public class EmailService(
        IConfigurationService configurationService,
        ICommonEmailService commonEmailService,
        IOptions<InstallEmailConfigurations> conf): IEmailService
    {
        private readonly string _welcomeMailSubject = conf.Value.Subject;

        public async Task SendWelcomeMailAsync(string customerName, string customerEmail)
        {
            // var message = await configurationService.GetInstallEmailAsync();
            // TODO Message
            var message = "await _configurationService.GetInstallEmailAsync()";

            await commonEmailService.SendAsync(
                new EmailAddress(customerName, customerEmail),
                _welcomeMailSubject,
                message);
        }
    }
}