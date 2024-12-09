using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using POD.Common.Database;
using POD.Functions.Payment.Schedule;

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
            (sendGridClientOptions) =>
            {
                sendGridClientOptions.ApiKey = configuration["SendGridApiKey"] ?? string.Empty;
            },
            (emailConfigurations) =>
            {
                emailConfigurations.FromEmail = configuration["Email_FromEmail"] ?? string.Empty;
                emailConfigurations.FromName = configuration["Email_FromName"] ?? string.Empty;

                bool.TryParse(configuration["Email_IsEmailHtml"], out bool isHtml);
                emailConfigurations.IsHtml = isHtml;
            },
            (serviceBusClientOptions) =>
            {
                serviceBusClientOptions.ConnectionString = configuration["ServiceBusConnectionString"] ?? string.Empty;
            },
            (configurationNames) =>
            {
                // TODO Configuration
                configurationNames.PaypalAccountNotFoundEmailConfigurationName = configuration["Email_PaymentErrorEmailSubject"] ?? string.Empty;
            },
            (paymentErrorEmailConfigurations) =>
            {
                paymentErrorEmailConfigurations.Subject = configuration["Email_PaymentErrorEmailSubject"] ?? string.Empty;
            },
            (paymentProcessingOptions) =>
            {
                paymentProcessingOptions.CapturePaymentRetryCount =
                    int.Parse(configuration["Payment_CapturePaymentRetryCount"] ?? string.Empty);
            },
            (queueNames) =>
            {
                queueNames.StripeCallExecutesQueueName = configuration["Payment_StripeCallExecutesQueueName"] ?? string.Empty;
                queueNames.BraintreeCallExecutesQueueName = configuration["Payment_BraintreeCallExecutesQueueName"] ?? string.Empty;
                queueNames.PayPalCallExecutesQueueName = configuration["Payment_PayPalCallExecutesQueueName"] ?? string.Empty;
            }
            );
    })
    .Build();

host.Run();