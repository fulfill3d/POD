using POD.Functions.Shopify.UpdateInventory.Data.Models;

namespace POD.Functions.Shopify.UpdateInventory.Services.Interfaces
{
    public interface IServiceBusService
    {
        Task SendUpdateInventoryCallExecuteMessage(string sessionId, UpdateInventoryServiceMessage message);
    }
}