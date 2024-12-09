using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using POD.Functions.Payment.Processing.Braintree;

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
            (serviceOption) =>
            {
                serviceOption.ConnectionString = configuration["ServiceBusConnectionString"] ?? string.Empty;
            },
            (queueNames) =>
            {
                queueNames.BraintreePaymentResultsQueueName = configuration["Payment_BraintreePaymentResultsQueueName"] ?? string.Empty;
            },
            (braintree) =>
            {
                braintree.UseSandbox = Boolean.Parse(configuration["Payment_UseBraintreeSandbox"] ?? string.Empty);
                braintree.MerchantId = configuration["Payment_BraintreeMerchantId"] ?? string.Empty;
                braintree.PublicKey = configuration["Payment_BraintreePublicKey"] ?? string.Empty;
                braintree.PrivateKey = configuration["Payment_BraintreePrivateKey"] ?? string.Empty;
            }
            );
    })
    .Build();

host.Run();