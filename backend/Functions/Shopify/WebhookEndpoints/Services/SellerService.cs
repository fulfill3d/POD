using Microsoft.EntityFrameworkCore;
using POD.Common.Database.Models;
using POD.Functions.Shopify.WebhookEndpoints.Data.Database;
using POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces;

namespace POD.Functions.Shopify.WebhookEndpoints.Services
{
    public class SellerService(WebhookEndpointsContext dbContext) : ISellerService
    {
        public async Task<int> NewOrExistingSeller(POD.Common.Database.Models.User user)
        {
            var doesExist = await dbContext.Sellers.AnyAsync(s => s.IsEnabled && s.UserId == user.Id);

            if (!doesExist)
            {
                var newSeller = new Seller
                {
                    UserId = user.Id,
                    UserRefId = user.RefId,
                    Discount = decimal.Zero,
                    HasBeenUpdated = true,
                    Status = "OK",
                    IsEnabled = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                };

                await dbContext.Sellers.AddAsync(newSeller);
                await dbContext.SaveChangesAsync();
                
                return newSeller.Id;
            }

            return await dbContext.Sellers
                .Where(s => s.IsEnabled && s.UserId == user.Id)
                .Select(s => s.Id)
                .FirstOrDefaultAsync();
        }
    }
}
