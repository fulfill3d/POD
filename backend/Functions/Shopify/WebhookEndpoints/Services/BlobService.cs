using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using POD.Functions.Shopify.WebhookEndpoints.Service.Options;
using POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces;
using POD.Integrations.BlobClient.Interface;

namespace POD.Functions.Shopify.WebhookEndpoints.Services
{
    public class BlobService(
        IBlobClient blobClient,
        ILogger<BlobService> logger,
        IOptions<ShopifyApiEndpointsOptions> opt) : IBlobService
    {
        private readonly string _newOrderErrorBlobName = opt.Value.OrderErrorBlobName;

        public async Task SaveShopifyOrderError(string uniqName, string blob)
        {
            try
            {
                // TODO: Generate a Blob and then save it
                // await blobClient.SaveStringToBlob(newOrderErrorBlobName, uniqName, blob);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
