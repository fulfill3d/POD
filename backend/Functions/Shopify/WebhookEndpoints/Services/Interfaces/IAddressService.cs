using POD.Integrations.ShopifyClient.Model;

namespace POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces
{
    public interface IAddressService
    {
        Task<Common.Database.Models.SellerAddress> CreateAddress(ShopifySeller shopifySeller);
    }
}
