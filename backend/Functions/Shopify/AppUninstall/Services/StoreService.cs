using Microsoft.EntityFrameworkCore;
using POD.Common.Service.Interfaces;
using POD.Functions.Shopify.AppUninstall.Data.Database;
using POD.Functions.Shopify.AppUninstall.Services.Interfaces;

namespace POD.Functions.Shopify.AppUninstall.Services
{
    public class StoreService(AppUninstallContext dbContext, ICommonStoreProductService commonStoreProductService): IStoreService
    {
        public async Task<POD.Common.Database.Models.Store> Get(string store)
        {
            return await dbContext.Stores.FirstOrDefaultAsync(s => s.IsEnabled && s.Name == store);
        }

        public async Task<bool> Delete(POD.Common.Database.Models.Store store)
        {
            // TODO Remove Store Products and all Related too
            store.Token = String.Empty;
            store.IsEnabled = false;
            store.LastModifiedDate = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            await commonStoreProductService.DeleteAllStoreProductsByStoreId(store.Id);

            return true;
        }
    }
}