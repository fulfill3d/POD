namespace POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces
{
    public interface IProductService
    {
        Task DeleteProduct(string json, string shop, CancellationToken cancellationToken);
    }
}
