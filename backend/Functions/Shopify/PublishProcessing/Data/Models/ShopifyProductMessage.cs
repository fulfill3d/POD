using POD.Functions.Shopify.PublishProcessing.Data.Models.Shopify.Product;

namespace POD.Functions.Shopify.PublishProcessing.Data.Models
{
    public class ShopifyProductMessage
    {
        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        public Product Product { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public int StoreId { get; set; } // TODO SellerId 

        /// <summary>
        /// Gets or sets the store.
        /// </summary>
        /// <value>
        /// The store.
        /// </value>
        public string Store { get; set; }

        /// <summary>
        /// Gets or sets the customer product identifier.
        /// </summary>
        /// <value>
        /// The customer product identifier.
        /// </value>
        public int StoreProductId { get; set; } // TODO StoreProductId 

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is product exist.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is product exist; otherwise, <c>false</c>.
        /// </value>
        public bool IsProductExist { get; set; }
    }
}
