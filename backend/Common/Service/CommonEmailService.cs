using Microsoft.Extensions.Options;
using POD.Common.Core.Option;
using POD.Common.Service.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using EmailAddress = POD.Common.Core.Model.EmailAddress;
using SendGridEmailAddress = SendGrid.Helpers.Mail.EmailAddress;

namespace POD.Common.Service
{
    public class CommonEmailService(
        ISendGridClient sendGridClient,
        IOptions<CommonEmailServiceEmailConfigurations> emailConfigurations): ICommonEmailService
    {
        private readonly string _fromEmail = emailConfigurations.Value.FromEmail;
        private readonly string _fromName = emailConfigurations.Value.FromName;
        private readonly bool _isHtml = emailConfigurations.Value.IsHtml;

        public async Task SendAsync(EmailAddress emailAddress, string subject, string message)
        {
            await SendAsync(new List<EmailAddress> { emailAddress }, subject, message);
        }

        // TODO: Add error handling here
        public async Task SendAsync(IEnumerable<EmailAddress> emailAddress, string subject, string content)
        {
            var msg = new SendGridMessage();
            var sendGridRecipients = emailAddress
                .Select(recipient => new SendGridEmailAddress()
                {
                    Name = recipient.Name,
                    Email = recipient.Email
                }).ToList();

            msg.SetFrom(new SendGridEmailAddress(_fromEmail,_fromName));
            msg.AddTos(sendGridRecipients);
            msg.SetSubject(subject);

            if (_isHtml)
                msg.AddContent(MimeType.Html, content);
            else
                msg.AddContent(MimeType.Text, content);

            await sendGridClient.SendEmailAsync(msg);   
        }
        
        public async Task SendAsync(EmailAddress recipient, string templateId, object dynamicTemplateData)
        {
            await SendAsync(new List<EmailAddress> { recipient }, templateId, dynamicTemplateData);
        }
        
        public async Task SendAsync(IEnumerable<EmailAddress> emailAddress, string templateId, object dynamicTemplateData)
        {
            var sendGridRecipients = emailAddress
                .Select(recipient => new SendGridEmailAddress()
                {
                    Name = recipient.Name,
                    Email = recipient.Email
                }).ToList();

            var msg = new SendGridMessage();
            msg.SetFrom(new SendGridEmailAddress(_fromEmail,_fromName));
            msg.AddTos(sendGridRecipients);
            msg.SetTemplateId(templateId);
            msg.SetTemplateData(dynamicTemplateData);

            await sendGridClient.SendEmailAsync(msg);
        }
    }
}