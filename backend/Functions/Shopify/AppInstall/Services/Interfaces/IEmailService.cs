namespace POD.Functions.Shopify.AppInstall.Services.Interfaces
{
    public interface IEmailService
    {
        public Task SendWelcomeMailAsync(string customerName, string customerEmail);
    }
}