using Microsoft.EntityFrameworkCore;
using POD.Functions.Shopify.WebhookEndpoints.Data.Database;
using POD.Functions.Shopify.WebhookEndpoints.Data.Models;
using POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces;

namespace POD.Functions.Shopify.WebhookEndpoints.Services
{
    public class UserService(WebhookEndpointsContext dbContext): IUserService
    {
        public async Task<POD.Common.Database.Models.User> NewOrExistingUser(B2CUser user)
        {
            var existingUser = await dbContext.Users
                .FirstOrDefaultAsync(u => u.IsEnabled == true && u.RefId == Guid.Parse(user.OID));

            if (existingUser != null)
            {
                return existingUser;
            }
            
            var newUser = new POD.Common.Database.Models.User
            {
                RefId = Guid.Parse(user.OID),
                FirstName = user.GivenName,
                LastName = user.FamilyName,
                IsEnabled = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Email = user.Email,
                Phone = "to be provided",
                IsPrivacyPolicyAccepted = false,
                HasTakenTour = false,
            };

            await dbContext.Users.AddAsync(newUser);

            await dbContext.SaveChangesAsync();
            // TODO New User Message
            return newUser;
        }
    }
}