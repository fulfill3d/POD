namespace POD.API.Seller.Product.Data.Models
{
    public class ProductView
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? Url { get; set; }
        public string? Tags { get; set; }
        public string? Title { get; set; }
        public string? Type { get; set; }
        public string? BodyHtml { get; set; }
        public IEnumerable<ProductVariantView> ProductVariantViews { get; set; }
    }

    public class ProductVariantView
    {
        public int Id { get; set; }
        public decimal? Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int SellerProductId { get; set; }
        public string? Name { get; set; }
        public string? Tags { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public string? Material { get; set; }
        public decimal? ShippingPrice { get; set; }
        public decimal? Weight { get; set; }
        public int WeightUnitId { get; set; }
        public IEnumerable<ProductImageView> ProductImageViews { get; set; }
        public IEnumerable<ProductPieceView> ProductPieceViews { get; set; }
    }

    public class ProductImageView
    {
        public int Id { get; set; }
        public string Url { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int ImageTypeId { get; set; }
        public string? Name { get; set; }
        public string? Alt { get; set; }
        public int SellerProductVariantId { get; set; }
        public bool IsDefaultImage { get; set; }
    }

    public class ProductPieceView
    {
        public int Id { get; set; }
        public DateTime ProductPieceCreatedAt { get; set; }
        public DateTime ProductPieceUpdatedAt { get; set; }
        public string FilamentName { get; set; }
        public string FilamentBrand { get; set; }
        public string FilamentDescription { get; set; }
        public string FilamentColor { get; set; }
        public string FilamentMaterial { get; set; }
        public string FilamentMaterialDescription { get; set; }
        public string ModelName { get; set; }
        public string ModelUri { get; set; }
        public string ModelType { get; set; }
        public long ModelSize { get; set; }
        public DateTime? ModelCreatedAt { get; set; }
        public decimal? ModelVolume { get; set; }
        public int VolumeUnitId { get; set; }
    }
}