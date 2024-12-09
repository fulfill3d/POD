using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using POD.Functions.Payment.Processing.Stripe;

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
            (serviceBusClientOptions) =>
            {
                serviceBusClientOptions.ConnectionString = configuration["ServiceBusConnectionString"] ?? string.Empty;
            },
            (queueNames) =>
            {
                queueNames.StripePaymentResultsQueueName = configuration["Payment_StripePaymentResultsQueueName"] ?? string.Empty;
            },
            (stripe) =>
            {
                stripe.ApiKey = configuration["Payment_StripeApiKey"] ?? string.Empty;
            }
            );
    })
    .Build();

host.Run();