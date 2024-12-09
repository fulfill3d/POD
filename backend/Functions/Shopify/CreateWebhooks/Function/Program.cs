using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using POD.Common.Database;
using POD.Functions.Shopify.CreateWebhooks;
using POD.Functions.Shopify.CreateWebhooks.Data;

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
            (config) =>
            {
                config.ShopifyWebhookEndpointsBaseUrl = configuration["ShopifyWebhookEndpointsBaseUrl"] ?? string.Empty;
                config.PodShopifyInventoryTrackingUpdatesCallbackUrl = configuration["PodShopifyInventoryTrackingUpdatesCallbackUrl"] ?? string.Empty;
                config.PodFullfilmentHandleName = configuration["PodFulfillmentHandleName"] ?? string.Empty;
                config.PodFullfilmentServiceName = configuration["PodFulfillmentServiceName"] ?? string.Empty;
                config.ShopifyWriteInventoryScopeName = configuration["ShopifyWriteInventoryScopeName"] ?? string.Empty;
            });
    })
    .Build();

host.Run();