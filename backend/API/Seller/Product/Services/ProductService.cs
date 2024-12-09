using Microsoft.EntityFrameworkCore;
using POD.API.Common.Core;
using POD.API.Seller.Product.Data.Database;
using POD.API.Seller.Product.Data.Models;
using POD.API.Seller.Product.Services.Interfaces;
using POD.Common.Database.Models;

namespace POD.API.Seller.Product.Services
{
    public class ProductService(ProductContext dbContext) : IProductService
    {
        public async Task<PagedResponse<ProductView>> GetProducts(Guid userRefId, Pagination pagination)
        {
                var productViewList = await dbContext.SellerProducts
                    .Include(sp => sp.SellerProductVariants)
                    .ThenInclude(spv => spv.SellerProductVariantImages)
                    .Where(sp => sp.UserRefId == userRefId && sp.IsEnabled)
                    .Select(sp => new ProductView {
                            Id = sp.Id,
                            CreatedAt = sp.CreatedAt,
                            UpdatedAt = sp.UpdatedAt,
                            Description = sp.Description,
                            Url = sp.Url,
                            Tags = sp.Tags,
                            Title = sp.Title,
                            Type = sp.Type,
                            BodyHtml = sp.BodyHtml,
                            ProductVariantViews = sp.SellerProductVariants
                                .Where(spv => spv.IsEnabled)
                                .Select(spv =>new ProductVariantView 
                                {
                                    Id = spv.Id,
                                    Price = spv.Price,
                                    CreatedDate = spv.CreatedDate,
                                    LastModifiedDate = spv.LastModifiedDate,
                                    SellerProductId = spv.SellerProductId,
                                    Name = spv.Name,
                                    Tags  = spv.Tags,
                                    Color = spv.Color,
                                    Size = spv.Size,
                                    Material = spv.Material,
                                    ShippingPrice = spv.ShippingPrice,
                                    Weight = spv.Weight,
                                    WeightUnitId = spv.WeightUnitId,
                                    ProductImageViews = spv.SellerProductVariantImages
                                        .Where(spvi => spvi.IsEnabled == true)
                                        .Select(spvi => new ProductImageView 
                                        {
                                            Id = spvi.Id,
                                            Url = spvi.Url,
                                            CreatedAt = spvi.CreatedAt,
                                            UpdatedAt = spvi.UpdatedAt,
                                            ImageTypeId = spvi.ImageTypeId,
                                            Name = spvi.Name,
                                            Alt = spvi.Alt,
                                            SellerProductVariantId = spvi.SellerProductVariantId,
                                            IsDefaultImage = spvi.IsDefaultImage,
                                        }), 
                                }) 
                    })
                    .Skip(pagination.PageSize * (pagination.PageNumber - 1))
                    .Take(pagination.PageSize)
                    .ToListAsync();
                
                return new PagedResponse<ProductView>
                {
                    Data = productViewList,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize,
                    TotalCount = productViewList.Count
                };
        }

        public async Task<ProductView?> GetProduct(Guid userRefId, int sellerProductId)
        {
            var productView = await dbContext.SellerProducts
                .Include(sp => sp.SellerProductVariants)
                    .ThenInclude(spv => spv.SellerProductVariantImages)
                .Include(sp => sp.SellerProductVariants)
                    .ThenInclude(spv => spv.ProductPieces)
                    .ThenInclude(pp => pp.Filament)
                    .ThenInclude(pp => pp.Brand)
                .Include(sp => sp.SellerProductVariants)
                    .ThenInclude(spv => spv.ProductPieces)
                    .ThenInclude(pp => pp.Filament)
                    .ThenInclude(pp => pp.Material)
                .Include(sp => sp.SellerProductVariants)
                    .ThenInclude(spv => spv.ProductPieces)
                    .ThenInclude(pp => pp.Filament)
                    .ThenInclude(pp => pp.Color)
                .Include(sp => sp.SellerProductVariants)
                    .ThenInclude(spv => spv.ProductPieces)
                    .ThenInclude(pp => pp.ThreeDmodelFile)
                .Where(sp =>
                    sp.IsEnabled &&
                    sp.UserRefId == userRefId &&
                    sp.Id == sellerProductId)
                .Select(sp => new ProductView
                {
                    Id = sp.Id,
                    CreatedAt = sp.CreatedAt,
                    UpdatedAt = sp.UpdatedAt,
                    Description = sp.Description,
                    Url = sp.Url,
                    Tags = sp.Tags,
                    Title = sp.Title,
                    Type = sp.Type,
                    BodyHtml = sp.BodyHtml,
                    ProductVariantViews = sp.SellerProductVariants
                        .Where(spv => spv.IsEnabled)
                        .Select(spv => new ProductVariantView
                        {
                            Id = spv.Id,
                            Price = spv.Price,
                            CreatedDate = spv.CreatedDate,
                            LastModifiedDate = spv.LastModifiedDate,
                            SellerProductId = spv.SellerProductId,
                            Name = spv.Name,
                            Tags = spv.Tags,
                            Color = spv.Color,
                            Size = spv.Size,
                            Material = spv.Material,
                            ShippingPrice = spv.ShippingPrice,
                            Weight = spv.Weight,
                            WeightUnitId = spv.WeightUnitId,
                            ProductImageViews = spv.SellerProductVariantImages
                                .Where(spvi => spvi.IsEnabled == true)
                                .Select(spvi => new ProductImageView
                                {
                                    Id = spvi.Id,
                                    Url = spvi.Url,
                                    CreatedAt = spvi.CreatedAt,
                                    UpdatedAt = spvi.UpdatedAt,
                                    ImageTypeId = spvi.ImageTypeId,
                                    Name = spvi.Name,
                                    Alt = spvi.Alt,
                                    SellerProductVariantId = spvi.SellerProductVariantId,
                                    IsDefaultImage = spvi.IsDefaultImage,
                                }),
                            ProductPieceViews = spv.ProductPieces
                                .Where(pp => pp.IsEnabled == true)
                                .Select(pp => new ProductPieceView
                                {
                                    Id = pp.Id,
                                    ProductPieceCreatedAt = pp.CreatedAt,
                                    ProductPieceUpdatedAt = pp.UpdatedAt,
                                    FilamentName = pp.Filament.Name,
                                    FilamentBrand = pp.Filament.Brand.Name ?? "", // TODO Non null,
                                    FilamentDescription = pp.Filament.Description,
                                    FilamentColor = pp.Filament.Color.Name ?? "", // TODO Non null,
                                    FilamentMaterial = pp.Filament.Material.Name ?? "", // TODO Non null,
                                    FilamentMaterialDescription = pp.Filament.Material.Description ?? "", // TODO Non null
                                    ModelName = pp.ThreeDmodelFile.Name,
                                    ModelUri = pp.ThreeDmodelFile.Uri,
                                    ModelType = pp.ThreeDmodelFile.Type,
                                    ModelSize = pp.ThreeDmodelFile.Size,
                                    ModelCreatedAt = pp.ThreeDmodelFile.CreatedAt,
                                    ModelVolume = pp.ThreeDmodelFile.Volume,
                                    VolumeUnitId = pp.ThreeDmodelFile.VolumeUnitId
                                })
                        })
                }).FirstOrDefaultAsync();
            
            return productView;
        }
        
        public async Task<bool> CreateProduct(Guid userRefId, int sellerId, ProductRequest request)
        {
                var product = new SellerProduct
                {
                    IsEnabled = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    SellerId = sellerId,
                    Tags = request.Tags,
                    Title = request.Title,
                    Type = request.Type,
                    Description = request.Description,
                    BodyHtml = request.BodyHtml,
                    Url = "request.Url",
                    UserRefId = userRefId,
                    SellerProductVariants = request.Variants.Select(v => new SellerProductVariant
                    {
                        IsEnabled = true,
                        CreatedDate = DateTime.UtcNow,
                        LastModifiedDate = DateTime.UtcNow,
                        Name = v.Name,
                        Tags = v.Tags,
                        Price = 10, // TODO Determine
                        ShippingPrice = 5, // TODO Determine
                        Color = "#", // TODO Determine
                        Size = "regular", // TODO Determine
                        Material = "regular", // TODO Determine
                        SellerProductVariantImages = v.Images.Select(pvir => new SellerProductVariantImage
                        {
                            IsEnabled = true,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow,
                            ImageTypeId = (int)POD.Common.Core.Enum.ImageType.Original,
                            Name = pvir.Name,
                            Alt = pvir.Alt,
                            IsDefaultImage = pvir.IsDefaultImage,
                            Url = "" // TODO Generate
                        }).ToList(),
                        ProductPieces = v.Pieces.Select(ppr => new ProductPiece
                        {
                            IsEnabled = true,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow,
                            FilamentId = ppr.FilamentId,
                            ThreeDmodelFileId = ppr.ModelFileId
                        }).ToList(),
                    }).ToList()
                };

                await dbContext.SellerProducts.AddAsync(product);

                await dbContext.SaveChangesAsync();
        
                return true;
        }
        
        public async Task<bool> UpdateProduct(Guid userRefId, ProductRequest request)
        {
            if (request.Id == null) return false;
            
            var product = await dbContext.SellerProducts
                .Include(sp => sp.SellerProductVariants)
                .ThenInclude(spv => spv.SellerProductVariantImages)
                .Include(sp => sp.SellerProductVariants)
                .ThenInclude(spv => spv.ProductPieces)
                .FirstOrDefaultAsync(sp =>
                    sp.IsEnabled &&
                    sp.UserRefId == userRefId &&
                    sp.Id == request.Id);
            
            if (product == null) return false;
            
            // TODO what to update
            
            return true;
        }
        
        public async Task<bool> DeleteProduct(Guid userRefId, int sellerProductId)
        {

            var sellerProduct = await dbContext.SellerProducts
                .Include(sp => sp.SellerProductVariants)
                .ThenInclude(spv => spv.SellerProductVariantImages)
                .Include(sp => sp.SellerProductVariants)
                .ThenInclude(spv => spv.ProductPieces)
                .FirstOrDefaultAsync(sp => sp.IsEnabled && sp.Id == sellerProductId && sp.UserRefId == userRefId);
        
            if (sellerProduct == null)
            {
                return false;
            }
                
            sellerProduct.IsEnabled = false;
            sellerProduct.UpdatedAt = DateTime.UtcNow;
        
            foreach (var variant in sellerProduct.SellerProductVariants)
            {
                variant.IsEnabled = false;
                variant.LastModifiedDate = DateTime.UtcNow;
                    
                foreach (var image in variant.SellerProductVariantImages)
                {
                    image.IsEnabled = false;
                    image.UpdatedAt = DateTime.UtcNow;
                }
                    
                foreach (var piece in variant.ProductPieces)
                {
                    piece.IsEnabled = false;
                    piece.UpdatedAt = DateTime.UtcNow;
                }
                    
            }
                
            await dbContext.SaveChangesAsync();
                
            return true;
            
        }
        
        public async Task<bool> PostVariantToExistingProduct(
            Guid userRefId, 
            int sellerProductId, 
            ProductVariantRequest request)
        {

                var any = await dbContext.SellerProducts
                    .AnyAsync(sp => sp.IsEnabled && sp.UserRefId == userRefId && sp.Id == sellerProductId);

                if (!any) return false;
                
                var variant = new SellerProductVariant
                {
                    SellerProductId = sellerProductId,
                    IsEnabled = true,
                    CreatedDate = DateTime.UtcNow,
                    LastModifiedDate = DateTime.UtcNow,
                    Name = request.Name,
                    Tags = request.Tags,
                    Price = 0, // TODO Determine
                    ShippingPrice = 0, // TODO Determine
                    Color = "#", // TODO Determine
                    Size = "regular", // TODO Determine
                    Material = "regular", // TODO Determine
                    SellerProductVariantImages = request.Images.Select(pvir => new SellerProductVariantImage
                    {
                        IsEnabled = true,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        ImageTypeId = (int)POD.Common.Core.Enum.ImageType.Original,
                        Name = pvir.Name,
                        Alt = pvir.Alt,
                        IsDefaultImage = pvir.IsDefaultImage,
                        Url = "" // TODO Generate
                    }).ToList(),
                    ProductPieces = request.Pieces.Select(ppr => new ProductPiece
                    {
                        IsEnabled = true,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        FilamentId = ppr.FilamentId,
                        ThreeDmodelFileId = ppr.ModelFileId
                    }).ToList(),
                };

                await dbContext.SellerProductVariants.AddAsync(variant);
                
                return true;
        }
        
        public async Task<bool> UpdateVariant(Guid userRefId, ProductVariantRequest request)
        {
            if (request.Id == null) return false;
            
            var variant = await dbContext.SellerProductVariants
                .Include(sp => sp.SellerProductVariantImages)
                .Include(sp => sp.ProductPieces)
                .Include(sp => sp.SellerProduct)
                .FirstOrDefaultAsync(sp =>
                    sp.IsEnabled &&
                    sp.SellerProduct.UserRefId == userRefId &&
                    sp.Id == request.Id);
            
            if (variant == null) return false;
            
            // TODO what to update
            
            return true;
        }
        
        public async Task<bool> DeleteVariant(Guid userRefId, int variantId)
        {
            var variant = await dbContext.SellerProductVariants
                .Include(spv => spv.SellerProduct)
                .Include(spv => spv.ProductPieces)
                .FirstOrDefaultAsync(spv =>
                    spv.IsEnabled &&
                    spv.Id == variantId &&
                    spv.SellerProduct.UserRefId == userRefId);
                
            if (variant == null)
            {
                return false;
            }

            variant.IsEnabled = false;
            variant.LastModifiedDate = DateTime.UtcNow;
        
            foreach (var piece in variant.ProductPieces)
            {
                piece.IsEnabled = false;
                piece.UpdatedAt = DateTime.UtcNow;
            }
                
            await dbContext.SaveChangesAsync();
                
            return true;
        }

        private double DetermineVariantMaterialCost(ProductVariantRequest variant)
        {
            // TODO Calculate cost
            return 2.99;
        }
        
        private double DetermineVariantPrintCost(ProductVariantRequest variant)
        {
            // TODO Calculate cost
            return 4.99;
        }
        
        private decimal DetermineVariantCost(ProductVariantRequest variant)
        {
            // TODO Calculate cost
            var printCost = DetermineVariantPrintCost(variant);
            var materialCost = DetermineVariantMaterialCost(variant);
            var cost = printCost + materialCost;
            return Convert.ToDecimal(cost);
        }
        
        private decimal DetermineVariantShippingCost(ProductVariantRequest variant)
        {
            // TODO Calculate cost
            return Convert.ToDecimal(4.99);
        }
        
        private decimal DetermineVariantPrice(ProductVariantRequest variant)
        {
            // TODO Calculate cost
            return Convert.ToDecimal(15.99);
        }
    }
}