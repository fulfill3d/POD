using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using POD.Functions.Shopify.CallExecutes;

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
            (serviceBus) =>
            {
                serviceBus.ConnectionString = configuration["ServiceBusConnectionString"] ?? string.Empty;
            },
            (locationConfig) =>
            {
                locationConfig.PodFulfilmentServiceName = configuration["PodFulfillmentServiceName"] ?? string.Empty;
            },
            (queueNames) =>
            {
                queueNames.ShopifyUpdatePublishProcessQueueName = configuration["ShopifyUpdatePublishProcessQueueName"] ?? string.Empty;
                queueNames.ShopifyPostPublishProcessQueueName = configuration["ShopifyPostPublishProcessQueueName"] ?? string.Empty;
                queueNames.ShopifyCallExecutesQeueuName = configuration["ShopifyCallExecutesQueueName"] ?? string.Empty;
                queueNames.ShopifyFulfillOrdersByCustomerQueueName = configuration["ShopifyFulfillOrdersByCustomerQueueName"] ?? string.Empty;
                queueNames.ChangeOrderStatusAsFulfilledQueueName = configuration["ChangeOrderStatusAsFulfilledQueueName"] ?? string.Empty;
                queueNames.ShopifyUpdateInventoryQueueName = configuration["ShopifyUpdateInventoryQueueName"] ?? string.Empty;
            },
            updateInventoryOptions =>
            {
                updateInventoryOptions.DefaultInventoryLevel = int.Parse(configuration["PodDefaultInventoryLevel"] ?? string.Empty);
                updateInventoryOptions.InventoryUpdateLimit = int.Parse(configuration["PodInventoryUpdateLimit"] ?? string.Empty);
                updateInventoryOptions.ShopifyUpdateInventoryForAllVariants = bool.Parse(configuration["ShopifyUpdateInventoryForAllVariants"] ?? string.Empty);
                updateInventoryOptions.PodFulfillmentServiceName = configuration["PodFulfillmentServiceName"] ?? string.Empty;
                updateInventoryOptions.PodVendorName = configuration["PodVendorName"] ?? string.Empty;
            }
            );
    })
    .Build();

host.Run();