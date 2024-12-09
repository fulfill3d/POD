using POD.Common.Core.Model;

namespace POD.Common.Service.Interfaces
{
    public interface ICommonEmailService
    {
        Task SendAsync(EmailAddress recipient, string subject, string message);
        Task SendAsync(IEnumerable<EmailAddress> recipients, string subject, string content);
        Task SendAsync(EmailAddress recipient, string templateId, object dynamicTemplateData);
        Task SendAsync(IEnumerable<EmailAddress> emailAddress, string templateId, object dynamicTemplateData);
    }
}