using Microsoft.EntityFrameworkCore;
using POD.Functions.Shopify.CreateWebhooks.Data.Database;
using POD.Functions.Shopify.CreateWebhooks.Services.Interfaces;

namespace POD.Functions.Shopify.CreateWebhooks.Services
{
    public class StoreService(CreateWebhooksContext db): IStoreService
    {
        public async Task<bool> SetShopifyScopeUpdatedTrue(string shop)
        {
            var store = await GetByShopName(shop);
        
            if (store == null) return false;
        
            store.IsShopifyScopeUpdated = true;
        
            await db.SaveChangesAsync();
        
            return true;
        }
        
        private async Task<POD.Common.Database.Models.Store> GetByShopName(string shop)
        {
            return await db.Stores.FirstOrDefaultAsync(s => s.IsEnabled && s.ShopIdentifier == shop);
        
        }
    }
}