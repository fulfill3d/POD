using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using POD.API.User.Identity;
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
            (tokenConfig) =>
            {
                tokenConfig.TokenEndpoint = configuration["User_PostRegister_B2C_TokenEndpoint"] ?? string.Empty;
                tokenConfig.SignInUpPolicy = configuration["User_PostRegister_B2C_SignInUpPolicy"] ?? string.Empty;
                tokenConfig.UpdatePolicy = configuration["User_PostRegister_B2C_UpdatePolicy"] ?? string.Empty;
                tokenConfig.ClientId = configuration["User_PostRegister_B2C_ClientId"] ?? string.Empty;
                tokenConfig.ClientSecret = configuration["User_PostRegister_B2C_ClientSecret"] ?? string.Empty;
                tokenConfig.PostRegisterRedirectUri = configuration["User_PostRegister_B2C_RedirectUri"] ?? string.Empty;
                tokenConfig.PostUpdateRedirectUri = configuration["User_PostUpdate_RedirectUri"] ?? string.Empty;
                tokenConfig.Scope = configuration["User_PostRegister_B2C_Scope"] ?? string.Empty;
                tokenConfig.GrantType = configuration["User_PostRegister_B2C_GrantType"] ?? string.Empty;
            },
            (identityConfig) =>
            {
                identityConfig.PostRegisterRedirectUri = configuration["User_PostRegister_RedirectUri"] ?? string.Empty;
                identityConfig.PostUpdateRedirectUri = configuration["User_PostUpdate_RedirectUri"] ?? string.Empty;
            },
            (emailOptions) =>
            {
                emailOptions.FromEmail = configuration["Email_FromEmail"] ?? string.Empty;
                emailOptions.FromName = configuration["Email_FromName"] ?? string.Empty;
                
                var isHtml = true;
                if (bool.TryParse(configuration["Email_IsEmailHtml"], out bool parsedIsHtml))
                {
                    isHtml = parsedIsHtml;
                }
                emailOptions.IsHtml = isHtml;

            },
            (sendGridOpt) =>
            {
                sendGridOpt.ApiKey = configuration["SendGridApiKey"] ?? string.Empty;
            }
            );
    })
    .Build();

host.Run();