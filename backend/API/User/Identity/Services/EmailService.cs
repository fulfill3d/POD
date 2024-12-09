
using POD.API.User.Identity.Services.Interfaces;
using POD.Common.Core.Model;
using POD.Common.Service.Interfaces;

namespace POD.API.User.Identity.Services
{
    public class EmailService(ICommonEmailService commonEmailService): IEmailService
    {
        private readonly string _welcomeEmailSubjectTemplate = "Welcome, {0}!";
        private readonly string _welcomeEmailMessageTemplate = "Dear {0},\n\nWelcome to our service!";
        private readonly string _updateEmailSubjectTemplate = "Profile Update Notification, {0}";
        private readonly string _updateEmailMessageTemplate = "Dear {0},\n\nYour profile has been updated successfully.";

        public async Task SendWelcomeEmail(string email, string firstName, string lastName)
        {
            var fullName = $"{firstName} {lastName}";
            var mail = new EmailAddress(fullName, email);

            var subject = string.Format(_welcomeEmailSubjectTemplate, fullName);
            var message = string.Format(_welcomeEmailMessageTemplate, fullName);

            await commonEmailService.SendAsync(mail, subject, message);
        }

        public async Task SendProfileUpdatedEmail(string email, string firstName, string lastName)
        {
            var fullName = $"{firstName} {lastName}";
            var mail = new EmailAddress(fullName, email);

            var subject = string.Format(_updateEmailSubjectTemplate, fullName);
            var message = string.Format(_updateEmailMessageTemplate, fullName);

            await commonEmailService.SendAsync(mail, subject, message);
        }
    }
}