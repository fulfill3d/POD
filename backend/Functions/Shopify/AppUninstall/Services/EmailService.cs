using Microsoft.Extensions.Options;
using POD.Common.Core.Model;
using POD.Common.Service.Interfaces;
using POD.Functions.Shopify.AppUninstall.Services.Interfaces;
using POD.Functions.Shopify.AppUninstall.Services.Options;

namespace POD.Functions.Shopify.AppUninstall.Services
{
    public class EmailService(
        ICommonEmailService commonEmailService,
        ICommonConfigurationService commonConfigurationService,
        IOptions<EmailConfigurations> emailConfig): IEmailService
    {
        private readonly string _uninstallMailConfigName = emailConfig.Value.UninstallEmailConfigurationName;
        private readonly string _uninstallMailSubject = emailConfig.Value.UninstallEmailSubject;

        public async Task SendUninstallMail(string email, string name)
        {
            var confObject = await commonConfigurationService.GetByNameAsync(_uninstallMailConfigName);

            if (confObject != null)
            {
                var message = confObject.Configuration1;
                await commonEmailService.SendAsync(new EmailAddress(email, name), _uninstallMailSubject, message);
            }
        }
    }
}