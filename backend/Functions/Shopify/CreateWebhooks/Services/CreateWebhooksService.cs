using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using POD.Common.Utils.Extensions;
using POD.Functions.Shopify.CreateWebhooks.Data.Enum;
using POD.Functions.Shopify.CreateWebhooks.Data.Models;
using POD.Functions.Shopify.CreateWebhooks.Services.Interfaces;
using POD.Functions.Shopify.CreateWebhooks.Services.Options;
using POD.Integrations.ShopifyClient.Interface;
using POD.Integrations.ShopifyClient.Model;

namespace POD.Functions.Shopify.CreateWebhooks.Services
{
    public class CreateWebhooksService(
        IOptions<CreateWebhooksServiceOption> createWebhooksOption,
        ILogger<CreateWebhooksService> logger,
        IStoreService storeService,
        IShopifyWebhooksClientFactory shopifyWebhooksClientFactory,
        IShopifyAccessScopeClientFactory shopifyAccessScopeClientFactory,
        IShopifyFulfillmentServiceClientFactory shopifyFullfilmentServiceClientFactory
    ) : ICreateWebhooksService
    {
        private readonly string _shopifyAppUrl = createWebhooksOption.Value.ShopifyWebhookEndpointsBaseUrl;
        private readonly string _podShopifyInventoryTrackingUpdatesCallbackUrl = createWebhooksOption.Value.PodShopifyInventoryTrackingUpdatesCallbackUrl;
        private readonly string _podFulfillmentServiceName = createWebhooksOption.Value.PodFullfilmentServiceName;
        private readonly string _podFulfillmentHandleName = createWebhooksOption.Value.PodFullfilmentHandleName;
        private readonly string _writeInventoryScopeName = createWebhooksOption.Value.ShopifyWriteInventoryScopeName;
        
        public async Task<bool> RegisterWebhooks(CreateWebhookMessage request)
        {
            var shopifyWebhookEndpoints = new ShopifyWebhookEndpoints(_shopifyAppUrl);
            var shopifyWebhooksClient = shopifyWebhooksClientFactory.CreateClient(request.Shop, request.Token);

            logger.LogInformation($"Webhooks process for store: {request.Shop} begin.");

            await shopifyWebhooksClient.Create(PrepareWebhook(WebhookType.AppUninstalled, shopifyWebhookEndpoints.UninstallUrl));
            await shopifyWebhooksClient.Create(PrepareWebhook(WebhookType.OrdersCreate, shopifyWebhookEndpoints.OrderCreatedUrl));
            await shopifyWebhooksClient.Create(PrepareWebhook(WebhookType.OrdersDelete, shopifyWebhookEndpoints.OrderDeletedUrl));
            await shopifyWebhooksClient.Create(PrepareWebhook(WebhookType.ProductsDelete, shopifyWebhookEndpoints.DeleteProductsUrl));
            await shopifyWebhooksClient.Create(PrepareWebhook(WebhookType.ShopUpdate, shopifyWebhookEndpoints.ShopUpdateUrl));
            
            logger.LogInformation($"Check if store: {request.Shop} has been updated.");
            
            // Check write_inventory scope granted
             var scopeClient = shopifyAccessScopeClientFactory.CreateClient(request.Shop, request.Token);
            
             var scopesResponse = await scopeClient.GetAll();
            
             if (IsWriteInventoryScopeExists(scopesResponse.Data))
             {
                 await storeService.SetShopifyScopeUpdatedTrue(request.Shop);
             }
            
             logger.LogInformation($"Create fulfillment service for store: {request.Shop} begin.");
            
             //Check Pod Fulfillment Registered
             var shopifyFulfillmentClient = shopifyFullfilmentServiceClientFactory.CreateClient(request.Shop, request.Token);
            
             var fulfillments = await shopifyFulfillmentClient.GetAll();
            
             var isPodFullfillmentExists = PodFulfillmentExists(fulfillments.Data);
            
             if (!isPodFullfillmentExists)
             {
                 var fulfillmentServiceRequest = CreateShopifyFulfilmentServicesRequest();
                 await shopifyFulfillmentClient.Create(fulfillmentServiceRequest);
             }
            
             logger.LogInformation($"Create fulfillment service for store: {request.Shop} end.");
            
             logger.LogInformation($"Webhooks process for store: {request.Shop} end.");

            return true;
        }
        private Webhook PrepareWebhook(WebhookType type, string callbackUrl)
        {
            return new Webhook()
            {
                Address = callbackUrl,
                Topic = type.GetDescription(),
                Format = "json"
            };
        }

        private bool IsWriteInventoryScopeExists(IEnumerable<AccessScope> scopes)
        {
            return scopes.Any(s => s.Handle == _writeInventoryScopeName);
        }

        private bool PodFulfillmentExists(IEnumerable<FulfillmentService> fulfillmentData)
        {
            return fulfillmentData.Any(ffd => ffd.Name == _podFulfillmentServiceName);
        }

        private FulfillmentService CreateShopifyFulfilmentServicesRequest()
        {
            return new FulfillmentService
            {
                Name = _podFulfillmentServiceName,
                Handle = _podFulfillmentHandleName,
                CallbackUrl = _podShopifyInventoryTrackingUpdatesCallbackUrl,
                InventoryManagement = false,
                TrackingSupport = false,
                RequiresShippingMethod = true,
                FulfillmentOrdersOptIn = true,
                PermitsSkuSharing = false,
                Format = "json"
            };
        }
    }
}