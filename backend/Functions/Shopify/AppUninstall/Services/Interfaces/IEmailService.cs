namespace POD.Functions.Shopify.AppUninstall.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendUninstallMail(string email, string name);
    }
}