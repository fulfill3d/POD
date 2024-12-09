using Microsoft.Extensions.Options;
using POD.Functions.Shopify.CallExecutes.Data.Models;
using POD.Functions.Shopify.CallExecutes.Services.Interfaces;
using POD.Functions.Shopify.CallExecutes.Services.Options;
using POD.Integrations.ShopifyClient.Interface;
using POD.Integrations.ShopifyClient.Model;
using POD.Integrations.ShopifyClient.Model.Product;
using RestSharp;

namespace POD.Functions.Shopify.CallExecutes.Services
{
    public class ShopifyUpdateInventoryService(
        IOptions<UpdateInventoryOptions> opt,
        IShopifyProductClientFactory shopifyProductClientFactory,
        IShopifyInventoryLevelClientFactory shopifyInventoryLevelClientFactory,
        IShopifyLocationService locationService) : IShopifyUpdateInventoryService
    {
        private readonly UpdateInventoryOptions _updateInventoryOptions = opt.Value;

        public async Task<bool> UpdateInventoryForProduct(UpdateInventoryForProductMessage createdProduct)
        {
            var productClient = shopifyProductClientFactory.CreateClient(createdProduct.Shop, createdProduct.Token);
            var inventoryLevelClient = shopifyInventoryLevelClientFactory.CreateClient(createdProduct.Shop, createdProduct.Token);
            var location = await locationService.GetPodLocationAsync(createdProduct.Shop, createdProduct.Token);
            var product = (await productClient.Get(createdProduct.ProductOnlineStoreId)).Data;

            if (location == null) return true;
            if (product.Vendor != _updateInventoryOptions.PodVendorName) return false;

            foreach (var variant in product.Variants)
            {
                if (!ShouldVariantInventoryBeUpdated(variant)) continue;
                var inventoryItemId = variant.InventoryItemId.GetValueOrDefault(0);
                var result = await SetInventoryLevel(location.Id.GetValueOrDefault(), inventoryLevelClient, inventoryItemId);
                if (result != null && !_updateInventoryOptions.ShopifyUpdateInventoryForAllVariants) break;
            }

            return true;
        }

        private Task<RestResponse<InventoryLevel>> SetInventoryLevel(
            long locationId,
            IShopifyInventoryLevelClient inventoryClient,
            long inventoryItemId)
        {
            var updateInventory = new InventoryLevel
            {
                LocationId = locationId,
                InventoryItemId = inventoryItemId,
                Available = _updateInventoryOptions.DefaultInventoryLevel,
                DisconnectIfNecessary = true
            };

            var result = inventoryClient.Set(updateInventory);
            return result;
        }

        private bool ShouldVariantInventoryBeUpdated(ProductVariant variant)
        {
            return variant.FulfillmentService == _updateInventoryOptions.PodFulfillmentServiceName
                    && variant.InventoryQuantity <= _updateInventoryOptions.InventoryUpdateLimit
                    && variant.InventoryManagement != null;
        }
    }
}
