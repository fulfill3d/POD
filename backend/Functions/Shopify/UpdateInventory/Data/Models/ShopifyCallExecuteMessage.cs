namespace POD.Functions.Shopify.UpdateInventory.Data.Models
{
    public class ShopifyCallExecuteMessage<T> where T : class
    {
        public T Data { get; set; }

        public int MessageType { get; set; }
    }
}