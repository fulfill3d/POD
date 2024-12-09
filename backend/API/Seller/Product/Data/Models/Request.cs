using Newtonsoft.Json;

namespace POD.API.Seller.Product.Data.Models
{

    public class ProductRequest
    {
        // This is product id, not seller product id
        [JsonProperty("id")]
        public int? Id { get; set; }
        
        [JsonProperty("title")]
        public string? Title { get; set; }
        
        [JsonProperty("description")]
        public string? Description { get; set; }
        
        [JsonProperty("body-html")]
        public string? BodyHtml { get; set; }
        
        [JsonProperty("tags")]
        public string? Tags { get; set; }
        
        [JsonProperty("type")]
        public string? Type { get; set; }
        
        [JsonProperty("variants")]
        public List<ProductVariantRequest>? Variants { get; set; }
    }
    
    public class ProductVariantRequest
    {
        // This is productVariant id, not seller productVariant id
        [JsonProperty("id")]
        public int? Id { get; set; }
        
        [JsonProperty("product-id")]
        public int ProductId { get; set; }
        
        [JsonProperty("name")]
        public string? Name { get; set; }
        
        [JsonProperty("tags")]
        public string? Tags { get; set; }
        
        [JsonProperty("product-pieces")]
        public List<ProductPieceRequest>? Pieces { get; set; }
        
        [JsonProperty("product-variant-images")]
        public List<ProductVariantImageRequest>? Images { get; set; }
    }
    
    public class ProductVariantImageRequest
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("name")]
        public string? Name { get; set; }
        
        [JsonProperty("alt")]
        public string? Alt { get; set; }
        
        [JsonProperty("is-default-image")]
        public bool IsDefaultImage { get; set; }
    }
    
    public class ProductPieceRequest
    {
        [JsonProperty("filament-id")]
        public int FilamentId { get; set; }
        
        [JsonProperty("model-file-id")]
        public int ModelFileId { get; set; }
    }
}