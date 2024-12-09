using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using POD.Functions.Shopify.UpdateInventory;

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
            (configureServiceBus) =>
            {
                configureServiceBus.ConnectionString = configuration["ServiceBusConnectionString"] ?? string.Empty;
            },
            (updateinventoryConfig) =>
            {
                updateinventoryConfig.ShopifyCallExecutesQueueName = configuration["ShopifyCallExecutesQueueName"] ?? string.Empty;
            }
            );
    })
    .Build();

host.Run();