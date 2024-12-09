namespace POD.Functions.Shopify.AppInstall.Services.Interfaces
{
    public interface IConfigurationService
    {
        public Task<string> GetInstallEmailAsync();
    }
}