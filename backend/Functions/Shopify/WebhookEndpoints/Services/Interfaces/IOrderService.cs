using POD.Integrations.ShopifyClient.Model.Order;

namespace POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces
{
    public interface IOrderService
    {
        Task OrderCreated(Order json, string shop, CancellationToken cancellationToken);
        Task OrderDeleted(Order order, string shop, CancellationToken cancellationToken);
    }
}
