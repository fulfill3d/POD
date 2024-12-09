using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using POD.API.Seller.Store.Data.Database;
using POD.API.Seller.Store.Data.Models;
using POD.API.Seller.Store.Services.Interfaces;
using POD.Common.Core.Enum;
using POD.Common.Database.Models;

namespace POD.API.Seller.Store.Services
{
    public class StoreService(StoreContext dbContext) : IStoreService
    {
        public async Task<bool> CreateStoreProductBySellerProduct(int sellerId, int storeId, ProductRequest request)
        {
            // TODO Check if store.IsEnabled by storeId
            // TODO IMPORTANT RevertAllDbChanges in catch blocks with all dbContext code
            // TODO Check if the store is owned by seller
            try
            {
                List<StoreProductVariantImage> storeProductVariantImages;
                List<StoreProductVariant> storeProductVariants;
                
                var dbSellerProduct = await dbContext.SellerProducts
                    .Include(sp => sp.SellerProductVariants)
                    .ThenInclude(spv => spv.SellerProductVariantImages)
                    .FirstOrDefaultAsync(sp => sp.IsEnabled == true && sp.Id == request.Id);

                if (dbSellerProduct == null) {
                    return false;
                }

                var storeProduct = new StoreProduct {
                    IsEnabled = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    PublishingStatus = (int)PublishStatus.ProcessStarted,
                    LastPublishingStatusChangeDate = DateTime.UtcNow,
                    StoreId = storeId,
                    Tags = request.Tags ?? dbSellerProduct.Tags,
                    Title = request.Name ?? dbSellerProduct.Title,
                    Type = request.Type ?? dbSellerProduct.Type,
                    Description = request.Description ?? dbSellerProduct.Description,
                    BodyHtml = request.BodyHtml ?? dbSellerProduct.BodyHtml,
                    Url = dbSellerProduct.Url
                };
                
                await dbContext.StoreProducts.AddAsync(storeProduct);

                await dbContext.SaveChangesAsync();

                if (request.Variants != null && request.Variants.Count > 0)
                {
                    storeProductVariants = request.Variants
                        .Select(pvr => new StoreProductVariant {
                            IsEnabled = true,
                            Price = dbSellerProduct.SellerProductVariants
                                .Where(pv => pv.Id == pvr.Id)
                                .Select(pv => pv.Price)
                                .FirstOrDefault(),
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow,
                            StoreProductId = storeProduct.Id,
                            SellerProductVariantId = dbSellerProduct.SellerProductVariants
                                .Where(pv => pv.Id == pvr.Id)
                                .Select(pv => pv.Id)
                                .FirstOrDefault(),
                            Name = pvr.Name ?? dbSellerProduct.SellerProductVariants
                                .Where(pv => pv.Id == pvr.Id)
                                .Select(pv => pv.Name)
                                .FirstOrDefault(),
                            Tags = pvr.Tags ?? dbSellerProduct.SellerProductVariants
                                .Where(pv => pv.Id == pvr.Id)
                                .Select(pv => pv.Name)
                                .FirstOrDefault(),
                            Color = dbSellerProduct.SellerProductVariants
                                .Where(pv => pv.Id == pvr.Id)
                                .Select(pv => pv.Color)
                                .FirstOrDefault(),
                            Size = dbSellerProduct.SellerProductVariants
                                .Where(pv => pv.Id == pvr.Id)
                                .Select(pv => pv.Size)
                                .FirstOrDefault(),
                            Material = dbSellerProduct.SellerProductVariants
                                .Where(pv => pv.Id == pvr.Id)
                                .Select(pv => pv.Material)
                                .FirstOrDefault(),
                            ShippingPrice = dbSellerProduct.SellerProductVariants
                                .Where(pv => pv.Id == pvr.Id)
                                .Select(pv => pv.ShippingPrice)
                                .FirstOrDefault(),
                            Weight = dbSellerProduct.SellerProductVariants
                                .Where(pv => pv.Id == pvr.Id)
                                .Select(pv => pv.Weight)
                                .FirstOrDefault(),
                            WeightUnitId = dbSellerProduct.SellerProductVariants
                                .Where(pv => pv.Id == pvr.Id)
                                .Select(pv => pv.WeightUnitId)
                                .FirstOrDefault(),
                        })
                        .ToList();
                    
                    await dbContext.AddRangeAsync(storeProductVariants);

                    await dbContext.SaveChangesAsync();

                    var imageRequests = request.Variants
                        .SelectMany(v => v.Images ?? Enumerable.Empty<ProductVariantImageRequest>())
                        .ToList();
                    
                    if (imageRequests.Count > 0)
                    {
                        storeProductVariantImages = dbSellerProduct.SellerProductVariants
                            .SelectMany(pv => pv.SellerProductVariantImages)
                            .Where(pv => imageRequests
                                .Any(ir => ir.Id == pv.Id))
                            .ToList()
                            .Select(pvi => new StoreProductVariantImage
                            {
                                IsEnabled = true,
                                CreatedDate = DateTime.UtcNow,
                                LastModifiedDate = DateTime.UtcNow,
                                ImageTypeId = pvi.ImageTypeId,
                                Url = pvi.Url ?? "",
                                Name = pvi.Name,
                                Alt = pvi.Alt,
                                StoreProductVariantId = storeProductVariants
                                    .Where(spv => spv.SellerProductVariantId == pvi.SellerProductVariantId)
                                    .Select(spv => spv.Id)
                                    .FirstOrDefault(),
                                IsDefaultImage = pvi.IsDefaultImage
                            })
                            .ToList();

                        await dbContext.StoreProductVariantImages.AddRangeAsync(storeProductVariantImages);

                        await dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        storeProductVariantImages = dbSellerProduct.SellerProductVariants
                            .SelectMany(pv => pv.SellerProductVariantImages)
                            .ToList()
                            .Select(pvi => new StoreProductVariantImage
                            {
                                IsEnabled = true,
                                CreatedDate = DateTime.UtcNow,
                                LastModifiedDate = DateTime.UtcNow,
                                ImageTypeId = pvi.ImageTypeId,
                                Url = pvi.Url ?? "",
                                Name = pvi.Name,
                                Alt = pvi.Alt,
                                StoreProductVariantId = storeProductVariants
                                    .Where(spv => spv.SellerProductVariantId == pvi.SellerProductVariantId)
                                    .Select(spv => spv.Id)
                                    .FirstOrDefault(),
                                IsDefaultImage = pvi.IsDefaultImage
                            })
                            .ToList();

                        await dbContext.StoreProductVariantImages.AddRangeAsync(storeProductVariantImages);

                        await dbContext.SaveChangesAsync();
                    }
                }
                else
                {
                    storeProductVariants = dbSellerProduct.SellerProductVariants
                        .Select(pv => new StoreProductVariant {
                            IsEnabled = true,
                            Price = pv.Price,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow,
                            StoreProductId = storeProduct.Id,
                            SellerProductVariantId = pv.Id,
                            Name = pv.Name,
                            Tags = pv.Tags,
                            Color = pv.Color,
                            Size = pv.Size,
                            Material = pv.Material,
                            ShippingPrice = pv.ShippingPrice,
                            Weight = pv.Weight,
                            WeightUnitId = pv.WeightUnitId,
                            Sku = GenerateVariantSku()
                        })
                        .ToList();
                    
                    await dbContext.StoreProductVariants.AddRangeAsync(storeProductVariants);

                    await dbContext.SaveChangesAsync();
                    
                    storeProductVariantImages = dbSellerProduct.SellerProductVariants
                        .SelectMany(pv => pv.SellerProductVariantImages)
                        .ToList()
                        .Select(spvi => new StoreProductVariantImage
                        {
                            IsEnabled = true,
                            CreatedDate = DateTime.UtcNow,
                            LastModifiedDate = DateTime.UtcNow,
                            ImageTypeId = spvi.ImageTypeId,
                            Url = spvi.Url ?? "",
                            Name = spvi.Name,
                            Alt = spvi.Alt,
                            StoreProductVariantId = storeProductVariants
                                .Where(spv => spv.SellerProductVariantId == spvi.SellerProductVariantId)
                                .Select(spv => spv.Id)
                                .FirstOrDefault(),
                            IsDefaultImage = spvi.IsDefaultImage
                        })
                        .ToList();

                    await dbContext.StoreProductVariantImages.AddRangeAsync(storeProductVariantImages);

                    await dbContext.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception e)
            {
                dbContext.RevertAllChangesInTheContext();
                return false;
            }

        }
        
        private string GenerateVariantSku() {
            // Generate a GUID
            var guid = Guid.NewGuid();
        
            // Convert GUID to string
            var guidString = guid.ToString();
        
            // Hash the GUID string using SHA-256
            using var sha256Hash = SHA256.Create();
            
            var data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(guidString));
            var sb = new StringBuilder();
            foreach (var datum in data)
            {
                sb.Append(datum.ToString("x2"));
            }
            
            // Return the first 12 characters of the hash
            return $"SKU{sb.ToString().Substring(0, 24).ToUpper()}";
        }
    }
}