using POD.Common.Core.Model;
using POD.Common.Service.Interfaces;
using POD.Functions.Shopify.OrderProcessing.Services.Interfaces;

namespace POD.Functions.Shopify.OrderProcessing.Services
{
    public class EmailService(ICommonEmailService commonEmailService) : IEmailService
    {
        public async Task SendErrorToDevelopers(string errorReason, string orderData)
        {
            // TODO: Convert these to options
            var recipients = new List<EmailAddress>()
            {
                new EmailAddress("Abdurrahman G Yavuz", "abdurrahman.g.yavuz@gmail.com")
            };

            var subject = "Error while processing order.";
            var content = errorReason
                + Environment.NewLine
                + orderData;

            await commonEmailService.SendAsync(recipients, subject, content);
        }
    }
}
