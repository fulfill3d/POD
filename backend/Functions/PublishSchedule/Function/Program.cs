using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using POD.Common.Database;
using POD.Functions.PublishSchedule;

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
            (option) =>
            {
                option.ConnectionString = configuration["ServiceBusConnectionString"] ?? string.Empty;
            },
            (option) =>
            {
                option.MaxProducts = int.Parse(configuration["PublishScheduleMaxProducts"] ?? string.Empty);
                option.MaxRetryCount = int.Parse(configuration["PublishScheduleMaxRetryCount"] ?? string.Empty);
                option.MinRetryInterval = int.Parse(configuration["PublishScheduleMinRetryInterval"] ?? string.Empty);
                option.MaxRetryInterval = int.Parse(configuration["PublishScheduleMaxRetryInterval"] ?? string.Empty);
            },
            (option) =>
            {
                option.ShopifyStartPublishProcessQueueName = configuration["ShopifyStartPublishProcessQueueName"] ?? string.Empty;
                option.EtsyStartPublishProcessQueueName = configuration["EtsyStartPublishProcessQueueName"] ?? string.Empty;
            }
            );
    })
    .Build();

host.Run();