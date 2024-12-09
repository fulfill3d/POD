using POD.Functions.Shopify.UpdateInventory.Data.Models;
using POD.Functions.Shopify.UpdateInventory.Services.Interfaces;

namespace POD.Functions.Shopify.UpdateInventory.Services
{
    public class UpdateInventoryService(IServiceBusService serviceBusService)
        : IUpdateInventoryService
    {

        public async Task CallUpdateInventory(UpdateInventoryProduct product)
        {
            var sessionId = product.StoreId.ToString();
            var message = new UpdateInventoryServiceMessage
            {
                ProductOnlineStoreId = product.ProductOnlineStoreId,
                Shop = product.Shop,
                Token = product.Token
            };

            await serviceBusService.SendUpdateInventoryCallExecuteMessage(sessionId, message);
        }
    }
}