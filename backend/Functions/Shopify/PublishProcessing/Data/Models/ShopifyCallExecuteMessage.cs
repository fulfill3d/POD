namespace POD.Functions.Shopify.PublishProcessing.Data.Models
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ShopifyCallExecuteMessage<T> where T : class
    {
        /// <summary>
        /// Gets or sets the publish product.
        /// </summary>
        /// <value>
        /// The publish product.
        /// </value>
        public T Data { get; set; }

        /// <summary>
        /// Gets or sets the type of the message.
        /// </summary>
        /// <value>
        /// The type of the message.
        /// </value>
        public int MessageType { get; set; }
    }
}
