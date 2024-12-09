using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POD.Common.Database.Models;
using POD.Functions.Shopify.WebhookEndpoints.Data.Database;
using POD.Functions.Shopify.WebhookEndpoints.Data.Models;
using POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces;

namespace POD.Functions.Shopify.WebhookEndpoints.Services
{
    public class StoreService(
        IServiceBusService serviceBusService,
        IShopifyService shopifyService,
        WebhookEndpointsContext dbContext,
        ILogger<StoreService> logger) : IStoreService
    {
        public async Task NewOrExistingStore(User user, int sellerId, string storeName, string code)
        {
            var doesAnyStoreExists = await dbContext.Stores.AnyAsync(s => s.IsEnabled && s.Name == storeName);

            if (!doesAnyStoreExists)
            {
                var accessToken = await shopifyService.GetAccessToken(storeName, code);
                var newStore = new Store
                {
                    SellerId = sellerId,
                    Name = storeName,
                    IsEnabled = true,
                    Token = accessToken,
                    UserRefId = user.RefId,
                    LastModifiedDate = DateTime.UtcNow,
                    IsTokenRevoked = false,
                    MarketPlaceId = (int)Common.Core.Enum.MarketPlace.Shopify,
                    ShopIdentifier = storeName,
                };

                await dbContext.Stores.AddAsync(newStore);
                await dbContext.SaveChangesAsync();
                
                await serviceBusService.SendCreateWebhookMessageToServiceBus(new CreateWebhook
                {
                    Token = accessToken,
                    Shop = storeName
                });
                await serviceBusService.SendInstallMessageToServiceBus(new NewInstallQueueMessage
                {
                    Shop = storeName,
                    Name = user.FirstName + user.LastName,
                    Email = user.Email ?? string.Empty,
                });
            }

            var sellerStoreMatch = await dbContext.Stores
                .Include(s => s.Seller)
                .AnyAsync(s => s.IsEnabled && s.Seller.Id == sellerId && s.Name == storeName);

            if (!sellerStoreMatch)
            {
                // TODO Exception
                throw new InvalidOperationException("Store is already added to another Seller");
            }
            
        }
        public async Task UpdateShop(string message, CancellationToken cancellationToken)
        {
            try
            {
                  await serviceBusService.SendUpdateShopMessageToServiceBus(message, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
