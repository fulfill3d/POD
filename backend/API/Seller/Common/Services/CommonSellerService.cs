using Microsoft.EntityFrameworkCore;
using POD.API.Seller.Common.Data;
using POD.API.Seller.Common.Services.Interfaces;

namespace POD.API.Seller.Common.Services
{
    public class CommonSellerService(SellerCommonContext dbContext) : ICommonSellerService
    {
        public async Task<int> GetSellerIdByUserId(Guid userId)
        {
            var seller = await dbContext.Sellers
                .FirstOrDefaultAsync(s => s.IsEnabled && s.UserRefId == userId);
            if (seller == null) return 0;

            return seller.Id;
        }
        
    
        public async Task<int> GetSellerIdByShop(string shop, POD.Common.Core.Enum.MarketPlace marketPlace)
        {
            var seller = await dbContext.Stores
                .Where(s => s.ShopIdentifier == shop &&
                            s.MarketPlaceId == (int)marketPlace &&
                            s.IsEnabled &&
                            s.Seller.IsEnabled)
                .Select(t => t.Seller)
                .FirstOrDefaultAsync();

            if (seller?.Id == null) return 0;

            return seller.Id;
        }
        public async Task<POD.Common.Database.Models.Seller> GetByCustomerIdAsync(int customerId)
        {
            return await dbContext.Sellers.FindAsync(customerId);
        }
        public async Task<POD.Common.Database.Models.Seller> GetByShop(string shop)
        {
            return await dbContext.Sellers
                .Include(s => s.Stores)
                .FirstOrDefaultAsync(s => s.IsEnabled && s.Stores.Any(s => s.Name == shop));
        }
        // public POD.Common.Database.Models.Seller GetByShop(string shop, int marketPlaceId)
        // {
        //     return _dbContext.Sellers
        //         .FirstOrDefault(c => c.IsEnabled && c.Shops
        //             .Any(s => s.MarketPlaceId == marketPlaceId && s.Name == shop && s.IsEnabled));
        // }

        public async Task<bool> IsCustomerBlocked(string shop)
        {
            // TODO BlockedUsers Entity is required
            // return await _dbContext.BlockedUsers.AnyAsync(x => x.IsEnabled && x.Shop.Equals(shop));
            return false;
        }

        public async Task<bool> SaveSellerStatusProcessing(POD.Common.Database.Models.Seller seller, string planName)
        {
            if (seller == null) return false; 

            seller.Status = planName;
            await dbContext.SaveChangesAsync();
            
            return true;
        }
    }
}