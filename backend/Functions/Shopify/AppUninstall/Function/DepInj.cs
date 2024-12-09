using Microsoft.Extensions.DependencyInjection;
using POD.Common.Core.Option;
using POD.Common.Database;
using POD.Common.Service;
using POD.Functions.Shopify.AppUninstall.Data.Database;
using POD.Functions.Shopify.AppUninstall.Services;
using POD.Functions.Shopify.AppUninstall.Services.Interfaces;
using POD.Functions.Shopify.AppUninstall.Services.Options;
using SendGrid;

namespace POD.Functions.Shopify.AppUninstall
{
    public static class DepInj
    {
        public static void RegisterServices(
            this IServiceCollection services,
            DbConnectionOptions dbConnectionsOptions,
            Action<EmailConfigurations> configureEmail,
            Action<CommonEmailServiceEmailConfigurations> configureCommonEmailService,
            Action<SendGridClientOptions> configureSendGrid)
        {
            #region Options

            services.ConfigureServiceOptions<EmailConfigurations>((_, options) => configureEmail(options));

            #endregion

            #region Services
            services.AddDatabaseContext<AppUninstallContext>(dbConnectionsOptions);
            
            services.AddCommonConfigurationService(dbConnectionsOptions);
            services.AddCommonStoreProductService(dbConnectionsOptions);
            services.AddCommonEmailService((_, options) => configureCommonEmailService(options), configureSendGrid);
            
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IStoreService, StoreService>();
            services.AddTransient<IAppUninstallService, AppUninstallService>();

            #endregion
        }
    }
}