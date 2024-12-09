using POD.Functions.Shopify.UpdateInventory.Data.Models;

namespace POD.Functions.Shopify.UpdateInventory.Services.Interfaces
{
    public interface IUpdateInventoryService
    {
        Task CallUpdateInventory(UpdateInventoryProduct product);
    }
}