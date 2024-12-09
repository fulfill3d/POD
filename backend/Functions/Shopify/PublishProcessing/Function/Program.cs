using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using POD.Common.Database;
using POD.Functions.Shopify.PublishProcessing;
using POD.Functions.Shopify.PublishProcessing.Data;

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
            (queueNames) =>
            {
                queueNames.ShopifyCallExecutesQueueName = configuration["ShopifyCallExecutesQueueName"] ?? string.Empty;
            },
            (podNames) =>
            {
                podNames.PodVendorName = configuration["PodVendorName"] ?? string.Empty;
                podNames.PodFulfillmentHandleName = configuration["PodFulfillmentHandleName"] ?? string.Empty;
                podNames.PodFulfillmentServiceName = configuration["PodFulfillmentServiceName"] ?? string.Empty;
                podNames.PodInventoryManagement = configuration["PodInventoryManagement"] ?? string.Empty;
                podNames.PodInventoryPolicy = configuration["PodInventoryPolicy"] ?? string.Empty;
            });
    })
    .Build();

host.Run();