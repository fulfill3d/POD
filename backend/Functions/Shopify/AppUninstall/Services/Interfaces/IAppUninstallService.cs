namespace POD.Functions.Shopify.AppUninstall.Services.Interfaces
{
    public interface IAppUninstallService
    {
        Task<bool> ProcessUninstall(string storeName);
    }
}