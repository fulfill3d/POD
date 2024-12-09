namespace POD.Functions.Shopify.CallExecutes.Services.Options
{
    public class QueueNames
    {
        public string ShopifyUpdatePublishProcessQueueName { get; set; }
        public string ShopifyPostPublishProcessQueueName { get; set; }
        public string ShopifyFulfillOrdersByCustomerQueueName { get; set; }
        public string ChangeOrderStatusAsFulfilledQueueName { get; set; }
        public string ShopifyCallExecutesQeueuName { get; set; }
        public string ShopifyUpdateInventoryQueueName { get; set; }
    }
}
