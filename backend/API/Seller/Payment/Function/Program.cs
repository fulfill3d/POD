using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using POD.API.Seller.Payment;
using POD.API.Seller.Payment.Data;
using POD.Common.Database;

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
            (paymentConfig) =>
            {
                paymentConfig.WaitingForConfirmationString = configuration["Payment_WaitingForConfirmationString"] ?? string.Empty;
            },
            (tokenOptions) =>
            {
                tokenOptions.MetadataUrl = configuration["B2C_MetadataUrl"] ?? string.Empty;
                tokenOptions.Issuer = configuration["B2C_Issuer"] ?? string.Empty;
                tokenOptions.ClientId = configuration["B2C_SellerFaaS_ClientId"] ?? string.Empty;
            },
            (tokenOptions) =>
            {
                tokenOptions.Read = configuration["AuthScope_Seller_Payment_Read"] ?? string.Empty;
                tokenOptions.Write = configuration["AuthScope_Seller_Payment_Write"] ?? string.Empty;
            },
            (braintree) =>
            {
                braintree.UseSandbox = Boolean.Parse(configuration["Payment_UseBraintreeSandbox"] ?? string.Empty);
                braintree.MerchantId = configuration["Payment_BraintreeMerchantId"] ?? string.Empty;
                braintree.PublicKey = configuration["Payment_BraintreePublicKey"] ?? string.Empty;
                braintree.PrivateKey = configuration["Payment_BraintreePrivateKey"] ?? string.Empty;
            },
            (stripe) =>
            {
                stripe.ApiKey = configuration["Payment_StripeApiKey"] ?? string.Empty;
            }
            );
    })
    .Build();

host.Run();