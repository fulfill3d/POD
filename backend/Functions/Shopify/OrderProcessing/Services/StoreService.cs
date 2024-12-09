using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using POD.Functions.Shopify.OrderProcessing.Data.Database;
using POD.Functions.Shopify.OrderProcessing.Data.Models.Shopify;
using POD.Functions.Shopify.OrderProcessing.Services.Interfaces;

namespace POD.Functions.Shopify.OrderProcessing.Services
{
    public class StoreService(
        IEmailService emailService,
        OrderProcessingContext dbContext) : IStoreService
    {
        public async Task<POD.Common.Database.Models.Store> GetStoreByShopIdentifier(string shop, Order newOrder)
        {
            var store = await dbContext.Stores.FirstOrDefaultAsync(s => s.IsEnabled && s.Name == shop);
            
            if (store == null)
            {
                var jsonOrder = JsonConvert.SerializeObject(newOrder);
                var errorText = "Shop:" + shop + " does not exist in the database.";
                await emailService.SendErrorToDevelopers(errorText, jsonOrder);
            }

            return store;
        }

    }
}
