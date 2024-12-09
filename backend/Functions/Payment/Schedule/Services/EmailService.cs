using Microsoft.Extensions.Options;
using POD.Common.Core.Model;
using POD.Common.Database.Models;
using POD.Common.Service.Interfaces;
using POD.Functions.Payment.Schedule.Services.Interfaces;
using POD.Functions.Payment.Schedule.Services.Options;

namespace POD.Functions.Payment.Schedule.Services
{
    public class EmailService(
        ICommonEmailService commonEmailService,
        ICommonUserService commonUserService,
        IConfigurationService _configurationService,
        IOptions<PaymentErrorEmailConfigurations> opt) : IEmailService
    {
        private readonly string _subject = opt.Value.Subject;

        public async Task NotifyStorePaymentError(Store store)
        {
            var user = await commonUserService.GetUserByStoreId(store.Id);
            if (user == null) return; // TODO Error handling
            if (user.Email == null) return; // TODO Error handling
            // var message = await configurationService.GetDefaultPaymentMethodNotFoundMailMessageAsync();
            // TODO SendGrid Message
            var message = "await configurationService.GetDefaultPaymentMethodNotFoundMailMessageAsync()";
            var name = $"{user.FirstName} {user.LastName}";
            await commonEmailService.SendAsync(
                new EmailAddress(name, user.Email),
                _subject,
                message);
        }
    }
}
