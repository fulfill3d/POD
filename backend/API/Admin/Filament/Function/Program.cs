using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using POD.API.Admin.Filament;
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

        services.AddFilamentServices(
            new DbConnectionOptions
            {
                ConnectionString = configuration["DatabaseConnectionString"] ?? string.Empty
            },
            (tokenOptions) =>
            {
                tokenOptions.MetadataUrl = configuration["B2C_MetadataUrl"] ?? string.Empty;
                tokenOptions.Issuer = configuration["B2C_Issuer"] ?? string.Empty;
                tokenOptions.ClientId = configuration["B2C_AdminFaaS_ClientId"] ?? string.Empty;
            },
            (tokenOptions) =>
            {
                tokenOptions.Read = configuration["AuthScope_Admin_Filament_Read"] ?? string.Empty;
                tokenOptions.Write = configuration["AuthScope_Admin_Filament_Write"] ?? string.Empty;
            });
    })
    .Build();

host.Run();