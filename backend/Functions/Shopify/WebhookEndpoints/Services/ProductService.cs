using Microsoft.Extensions.Logging;
using POD.Functions.Shopify.WebhookEndpoints.Data.Models;
using POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces;

namespace POD.Functions.Shopify.WebhookEndpoints.Services
{
    public class ProductService(
        IServiceBusService serviceBusService,
        ILogger<ProductService> logger) : IProductService
    {
        public async Task DeleteProduct(string json, string shop, CancellationToken cancellationToken)
        {
            try
            {
                var deletedProduct = new DeleteProduct
                {
                    Shop = shop,
                    Content = json
                };

                await serviceBusService.SendDeleteProductMessageToServiceBus(deletedProduct, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}