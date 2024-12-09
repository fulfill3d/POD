using Microsoft.EntityFrameworkCore;
using POD.Common.Core.Enum;
using POD.Common.Database.Models;
using POD.Common.Service.Interfaces;

namespace POD.Common.Service
{
    public class CommonStoreProductService(DatabaseContext dbContext): ICommonStoreProductService
    {
        public async Task<PublishStatus> GetStoreProductPublishStatus(int id)
        {
            var status = await dbContext.StoreProducts
                .Where(sp => sp.IsEnabled && sp.Id == id)
                .Select(sp => sp.PublishingStatus)
                .FirstOrDefaultAsync();
            
            return (PublishStatus)status;
        }

        public async Task SetStoreProductPublishStatus(int id, PublishStatus status)
        {
            var product = await dbContext.StoreProducts
                .Where(sp => sp.IsEnabled && sp.Id == id)
                .FirstOrDefaultAsync();
            
            if (product == null) return;

            product.PublishingStatus = (int)status;
            product.LastPublishingStatusChangeDate = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();
        }

        public async Task<StoreProduct?> GetStoreProductById(int storeProductId)
        {
            return await dbContext.StoreProducts
                .Where(sp => sp.IsEnabled && sp.Id == storeProductId)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetStoreProductIdByOnlineStoreProductId(string id)
        {
            return await dbContext.StoreProducts
                .Where(sp => sp.ProductOnlineStoreId == id)
                .Select(sp => sp.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetStoreProductVariantIdBySku(string sku)
        {
            return await dbContext.StoreProductVariants
                .Where(spv => spv.IsEnabled && spv.Sku == sku)
                .Select(spv => spv.Id)
                .FirstOrDefaultAsync();
        }

        public async Task DeleteStoreProductByOnlineStoreId(string onlineStoreProductId)
        {
            var product = await dbContext.StoreProducts
                .Include(sp => sp.StoreProductVariants.Where(spv => spv.IsEnabled))
                .ThenInclude(spv => spv.StoreProductVariantImages.Where(spvi => spvi.IsEnabled))
                .FirstOrDefaultAsync(sp => sp.IsEnabled && sp.ProductOnlineStoreId == onlineStoreProductId);
            
            if (product != null)
            {
                product.IsEnabled = false;
                product.UpdatedAt = DateTime.UtcNow;
    
                foreach (var variant in product.StoreProductVariants)
                {
                    variant.IsEnabled = false;
                    variant.UpdatedAt = DateTime.UtcNow;

                    foreach (var image in variant.StoreProductVariantImages)
                    {
                        image.IsEnabled = false;
                        image.LastModifiedDate = DateTime.UtcNow;
                    }
                    
                }

                await dbContext.SaveChangesAsync();
            }
        }

        public async Task SetStoreProductOnlineStoreIdById(int storeProductId, string onlineStoreProductId)
        {
            var product = await dbContext.StoreProducts
                .FirstOrDefaultAsync(sp => sp.IsEnabled && sp.Id == storeProductId);

            if (product != null)
            {
                product.ProductOnlineStoreId = onlineStoreProductId;
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task SetStoreProductVariantOnlineStoreProductVariantIdById(int storeProductVariantId,
            string onlineStoreProductVariantId)
        {
            var variant = await dbContext.StoreProductVariants
                .FirstOrDefaultAsync(spv => spv.IsEnabled && spv.Id == storeProductVariantId);

            if (variant != null)
            {
                variant.OnlineStoreProductVariantId = onlineStoreProductVariantId;
            }
            
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAllStoreProductsByStoreId(int storeId)
        {
            var products = await dbContext.StoreProducts
                .Include(sp => sp.StoreProductVariants.Where(spv => spv.IsEnabled))
                .ThenInclude(spv => spv.StoreProductVariantImages.Where(spvi => spvi.IsEnabled))
                .Where(sp => sp.IsEnabled && sp.StoreId == storeId)
                .ToListAsync();

            foreach (var product in products)
            {
                product.IsEnabled = false;
                product.UpdatedAt = DateTime.UtcNow;
                product.PublishingStatus = (int)PublishStatus.Idle;
    
                foreach (var variant in product.StoreProductVariants)
                {
                    variant.IsEnabled = false;
                    variant.UpdatedAt = DateTime.UtcNow;

                    foreach (var image in variant.StoreProductVariantImages)
                    {
                        image.IsEnabled = false;
                        image.LastModifiedDate = DateTime.UtcNow;
                    }
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}