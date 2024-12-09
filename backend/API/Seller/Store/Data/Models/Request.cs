using Newtonsoft.Json;

namespace POD.API.Seller.Store.Data.Models
{
    public class ProductRequest
    {
        // This is seller product id, not store product id
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("name")]
        public string? Name { get; set; }
        
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
        // This is sellerProductVariant id, not storeProductVariant id
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("product-id")]
        public int ProductId { get; set; }
        
        [JsonProperty("name")]
        public string? Name { get; set; }
        
        [JsonProperty("tags")]
        public string? Tags { get; set; }
        
        [JsonProperty("product-variant-images")]
        public List<ProductVariantImageRequest>? Images { get; set; }
    }
    
    public class ProductVariantImageRequest
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}