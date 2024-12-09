using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using POD.Common.Database;
using POD.Functions.Shopify.WebhookEndpoints;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration(builder =>
    {
        var configuration = builder.Build();
        var token = new DefaultAzureCredential();
        var appConfigUrl = configuration["AppConfigUrl"] ?? string.Empty;

        builder.AddAzureAppConfiguration(config =>
        {
            config.Connect(new Uri(appConfigUrl), token);
            config.ConfigureKeyVault(kv => kv.SetCredential(token));
        });
    })
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;

        services.RegisterServices(
                new DbConnectionOptions
                {
                    ConnectionString = configuration["DatabaseConnectionString"] ?? string.Empty
                },
                (serviceBus) =>
                {
                    serviceBus.ConnectionString = configuration["ServiceBusConnectionString"] ?? string.Empty;
                },
                (blobOptions) =>
                {
                    blobOptions.ConnectionString = configuration["BlobConnectionString"] ?? string.Empty;
                },
                (webhooksService) =>
                {
                    webhooksService.ShopifyWebhookEndpointsBaseUrl = configuration["ShopifyWebhookEndpointsBaseUrl"] ?? string.Empty;
                    webhooksService.PodFrontEndUrl = configuration["PodFrontEndUrl"] ?? string.Empty;
                    webhooksService.ShopifySecretKey = configuration["ShopifySecretKey"] ?? string.Empty;
                    webhooksService.ShopifyAPIKey = configuration["ShopifyAPIKey"] ?? string.Empty;
                    webhooksService.OrderErrorBlobName = configuration["OrderErrorBlobName"] ?? string.Empty;
                    webhooksService.ShopifyOrdersDeletedServiceBusName = configuration["ShopifyOrdersDeletedQueueName"] ?? string.Empty;
                    webhooksService.ShopifyStoreStatusUpdateQueueName = configuration["ShopifyStoreStatusUpdateQueueName"] ?? string.Empty;
                    webhooksService.ShopifyDeleteProductsServiceBusName = configuration["ShopifyDeleteProductsQueueName"] ?? string.Empty;
                    webhooksService.ShopifyCreateWebhooksServiceBusName = configuration["ShopifyCreateWebhooksQueueName"] ?? string.Empty;
                    webhooksService.ShopifyInstallServiceBusName = configuration["ShopifyInstallQueueName"] ?? string.Empty;
                    webhooksService.ShopifyOrderProcessingServiceBusName = configuration["ShopifyOrderProcessingQueueName"] ?? string.Empty;
                    webhooksService.ShopifyUninstallServiceBusName = configuration["ShopifyUninstallQueueName"] ?? string.Empty;
                },
                (tokenConfiguration) =>
                {
                    tokenConfiguration.ShopifyOauthHashSalt = configuration["ShopifyOauthHashSalt"] ?? string.Empty;
                    tokenConfiguration.ShopifyClaimType = configuration["ShopifyClaimType"] ?? string.Empty;
                },
                (requestTimeout) =>
                {
                    requestTimeout.TimeoutValue = int.Parse(configuration["ShopifyWebhookTimeoutValue"] ?? string.Empty);
                },
                (b2COptions) =>
                {
                    b2COptions.AuthorizationEndpoint = configuration["B2C_AuthorizationEndpoint"] ?? string.Empty;
                    b2COptions.TokenEndpoint = configuration["B2C_TokenEndpoint"] ?? string.Empty;
                    b2COptions.ClientId = configuration["B2C_ShopifyPostRegister_ClientId"] ?? string.Empty;
                    b2COptions.ClientSecret = configuration["B2C_ShopifyPostRegister_ClientSecret"] ?? string.Empty;
                    b2COptions.RedirectUri = configuration["B2C_ShopifyPostRegister_RedirectUri"] ?? string.Empty;
                    b2COptions.Nonce = configuration["B2C_Nonce"] ?? string.Empty;
                    b2COptions.Policy = configuration["B2C_Policy"] ?? string.Empty;
                    b2COptions.ResponseType = configuration["B2C_ResponseType"] ?? string.Empty;
                    b2COptions.Scope = configuration["B2C_Scope"] ?? string.Empty;
                    b2COptions.GrantType = configuration["B2C_GrantType"] ?? string.Empty;
                }
            );
    })
    .Build();

host.Run();