using Microsoft.EntityFrameworkCore;
using POD.Common.Database.Contexts;
using POD.Common.Database.Models;
using POD.Common.Service.Interfaces;

namespace POD.Common.Service
{
    public class CommonUserService(CommonUserServiceDbContext dbContext): ICommonUserService
    {
        public async Task<User?> GetUserByUserRefId(Guid guid)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.IsEnabled == true && u.RefId == guid);
        }

        public async Task<User?> GetUserBySellerId(int sellerId)
        {
            return await dbContext.Sellers
                .Include(u => u.User)
                .Where(s => s.IsEnabled && s.Id == sellerId && s.User.IsEnabled == true)
                .Select(s => s.User)
                .FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByStoreId(int storeId)
        {
            return await dbContext.Stores
                .Include(s => s.Seller)
                .ThenInclude(s => s.User)
                .Where(s => s.IsEnabled && s.Id == storeId && s.Seller.IsEnabled && s.Seller.User.IsEnabled == true)
                .Select(s => s.Seller.User)
                .FirstOrDefaultAsync();
        }
    }
}