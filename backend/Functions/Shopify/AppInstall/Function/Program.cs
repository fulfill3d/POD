using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using POD.Common.Database;
using POD.Functions.Shopify.AppInstall;

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
            (sendGridOptions) =>
            {
                sendGridOptions.ApiKey = configuration["SendGridApiKey"] ?? string.Empty;
            },
            (emailOptions) =>
            {
                emailOptions.FromEmail = configuration["Email_FromEmail"] ?? string.Empty;
                emailOptions.FromName = configuration["Email_FromName"] ?? string.Empty;

                bool.TryParse(configuration["Email_IsEmailHtml"], out bool isHtml);
                emailOptions.IsHtml = isHtml;
            },
            (configurationNames) =>
            {
                configurationNames.InstallEmailConfigName = configuration["InstallEmailConfigName"] ?? string.Empty;
            },
            (newInstallEmail) =>
            {
                newInstallEmail.Subject = configuration["InstallEmailSubject"] ?? string.Empty;
            }
            );
    })
    .Build();

host.Run();