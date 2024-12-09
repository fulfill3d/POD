using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using POD.Common.Database;
using POD.Functions.Shopify.OrderProcessing;

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

        services.RegisterServices(new DbConnectionOptions
            {
                ConnectionString = configuration["DatabaseConnectionString"] ?? string.Empty
            },
            (serviceClient) =>
            {
                serviceClient.ConnectionString = configuration["ServiceBusConnectionString"] ?? string.Empty;
            },
            (queueNames) =>
            {
                queueNames.ShopifyCallExecutesQueueName = configuration["ShopifyCallExecutesQueueName"] ?? string.Empty;
                queueNames.ShopifyUpdateInventoryQueueName = configuration["ShopifyUpdateInventoryQueueName"] ?? string.Empty;
            });
    })
    .Build();

host.Run();