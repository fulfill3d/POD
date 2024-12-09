using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using POD.Common.Utils.Extensions;
using POD.Functions.Shopify.PublishProcessing.Data.Models;
using POD.Functions.Shopify.PublishProcessing.Data.Database;
using POD.Functions.Shopify.PublishProcessing.Data.Models.Shopify.Product;
using POD.Functions.Shopify.PublishProcessing.Services.Interfaces;
using POD.Functions.Shopify.PublishProcessing.Services.Options;

namespace POD.Functions.Shopify.PublishProcessing.Services
{
    public class ProductService(
        PublishProcessingContext dbContext,
        IOptions<PublishProcessingOptions> opt): IProductService
    {
        private readonly string _podVendorName = opt.Value.PodVendorName;
        private readonly string _podFulfillmentHandleName = opt.Value.PodFulfillmentHandleName;
        private readonly string _podFulfillmentServiceName = opt.Value.PodFulfillmentServiceName;
        private readonly string _podInventoryManagement = opt.Value.PodInventoryManagement;
        private readonly string _podInventoryPolicy = opt.Value.PodInventoryPolicy;
        
        public async Task<ShopifyProductMessage?> PrepareShopifyProductMessage(
            PublishProductMessage publishProductMessage)
        {
            var storeProduct = await GetStoreProduct(publishProductMessage.StoreProductId);
            
            if (storeProduct == null) return null; // TODO Set PublishStatus backwards

            var store = await GetStore(publishProductMessage.StoreId);
            
            if (store == null) return null; // TODO Set PublishStatus backwards
            // TODO Sales Channels is only POS, Fix it
            return new ShopifyProductMessage
            {
                Product = GetShopifyProduct(storeProduct),
                StoreId = storeProduct.StoreId,
                Store = store.Name,
                StoreProductId = storeProduct.Id,
                Token = store.Token,
                IsProductExist = false
            };
        }

        public async Task<ShopifyProductMessage> PrepareShopifyUpdateProductMessage(
            ShopifyProductMessage shopifyProductMessage)
        {
            var storeProduct = await GetStoreProduct(shopifyProductMessage.StoreProductId);
            var counter = 0;

            shopifyProductMessage.Product.Images = storeProduct.StoreProductVariants
                .SelectMany(spv => spv.StoreProductVariantImages
                    .Select(spvi => new 
                    {
                    spv.Sku,
                    spvi 
                    }))
                .GroupBy(image => image.spvi)
                .OrderBy(image => image.Key.Id)
                .Select(image => new ProductImage
                {
                    Src = image.Key.Url, // TODO Non nullable
                    Position = ++counter,
                    VariantIds = shopifyProductMessage.Product.Variants
                        .Where(pv => image
                            .Any(x => x.spvi.IsDefaultImage && 
                                      x.Sku == pv.SKU))
                        .Select(pv => Convert.ToInt64(pv.Id))
                });

            return shopifyProductMessage;
        }
        
        private async Task<POD.Common.Database.Models.StoreProduct?> GetStoreProduct(int storeProductId)
        {
            return await dbContext.StoreProducts
                .Include(sp => sp.StoreProductVariants
                    .Where(spv => spv.IsEnabled))
                .ThenInclude(spv => spv.StoreProductVariantImages
                    .Where(spvi => spvi.IsEnabled))
                .FirstOrDefaultAsync(sp => sp.IsEnabled && sp.Id == storeProductId);
        }
        
        private async Task<POD.Common.Database.Models.Store?> GetStore(int storeId)
        {
            return await dbContext.Stores
                .Where(s => s.IsEnabled && s.Id == storeId)
                .FirstOrDefaultAsync();
        }

        private Product GetShopifyProduct(POD.Common.Database.Models.StoreProduct storeProduct)
        {
            return new Product {
                    Title = storeProduct.Title ?? "",
                    Vendor = _podVendorName,
                    Handle = _podFulfillmentHandleName, // TODO This is wrong. Handle is sth different, name of the product
                    BodyHtml = storeProduct.BodyHtml ?? "",
                    ProductType = storeProduct.Type ?? "",
                    Variants = GetShopifyProductVariants(storeProduct),
                    Options = GetShopifyOptions(),
                    Images = GetShopifyImages(storeProduct),
                    Tags = storeProduct.Tags ?? ""
                };
        }

        private IEnumerable<ProductVariant> GetShopifyProductVariants(
            POD.Common.Database.Models.StoreProduct storeProduct)
        {
            return storeProduct.StoreProductVariants.Select(spv => new ProductVariant
            {
                // TODO If Option1s has same value but others not (Material), Product cannot published, Must be fixed
                // may be by ProductOption in Product
                Title = spv.Name ?? "",
                SKU = spv.Sku,
                Option1 = spv.Color ?? "", // Those must be different for each variants
                Option2 = spv.Material ?? "",
                Option3 = spv.Size ?? "",
                Price = spv.Price ?? 0, // TODO Add Price to StoreProductVariant dB
                WeightUnit = ((POD.Common.Core.Enum.Unit)spv.WeightUnitId).GetDescription() ?? "",
                Weight = spv.Weight,
                FulfillmentService = _podFulfillmentServiceName, // Read Shopify Docs for those
                InventoryManagement = _podInventoryManagement, // Read Shopify Docs for those
                InventoryPolicy = _podInventoryPolicy // Read Shopify Docs for those
            });
        }

        private static IEnumerable<ProductOption> GetShopifyOptions()
        {
            // TODO IOptions<ServiceOpt>.Options
            // Options = storeProduct.StoreProductVariants.Select(spv => new ProductOption
            // {
            //     Name = "spv.Color"
            // }),
            return new List<ProductOption>();
        }

        private static IEnumerable<ProductImage> GetShopifyImages(
            POD.Common.Database.Models.StoreProduct storeProduct)
        {
            return new List<ProductImage>();
        }
    }
}