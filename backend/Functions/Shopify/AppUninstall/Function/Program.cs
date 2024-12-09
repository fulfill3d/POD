using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using POD.Common.Database;
using POD.Functions.Shopify.AppUninstall;
using POD.Functions.Shopify.AppUninstall.Data;

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
            (emailConfigurations) =>
            {
                emailConfigurations.UninstallEmailConfigurationName = configuration["UninstallEmailConfigName"] ?? string.Empty;
                emailConfigurations.UninstallEmailSubject = configuration["UninstallEmailSubject"] ?? string.Empty;
            },
            (commonEmailServiceEmailConfigurations) =>
            {
                commonEmailServiceEmailConfigurations.FromEmail = configuration["Email_FromEmail"] ?? string.Empty;
                commonEmailServiceEmailConfigurations.FromName = configuration["Email_FromName"] ?? string.Empty;

                bool.TryParse(configuration["Email_IsEmailHtml"], out bool isHtml);
                commonEmailServiceEmailConfigurations.IsHtml = isHtml;
            },
            (sendGridClientOptions) =>
            {
                sendGridClientOptions.ApiKey = configuration["SendGridApiKey"] ?? string.Empty;
            }
            );
    })
    .Build();

host.Run();