using Microsoft.Extensions.DependencyInjection;
using POD.API.Seller.Common.Services;
using POD.Common.Core.Option;
using POD.Common.Database;
using POD.Common.Service;
using POD.Functions.Shopify.AppInstall.Service.Options;
using POD.Functions.Shopify.AppInstall.Services;
using POD.Functions.Shopify.AppInstall.Services.Interfaces;
using SendGrid;

namespace POD.Functions.Shopify.AppInstall
{
    public static class DepInj
    {
        public static void RegisterServices(
            this IServiceCollection services,
            DbConnectionOptions dbConnectionsOptions,
            Action<SendGridClientOptions> configureSendGrid,
            Action<CommonEmailServiceEmailConfigurations> configureEmail,
            Action<ConfigurationNames> configurationNames,
            Action<InstallEmailConfigurations> emailConfigurations)
        {

            #region Options
            
            services.ConfigureServiceOptions<ConfigurationNames>((_, options) => configurationNames(options));
            services.ConfigureServiceOptions<InstallEmailConfigurations>((_, options) => emailConfigurations(options));
            
            #endregion

            #region Services
            
            services.AddCommonSellerServices(dbConnectionsOptions);
            services.AddCommonEmailService((_, options) => configureEmail(options), configureSendGrid);
            services.AddCommonConfigurationService(dbConnectionsOptions);
            
            services.AddTransient<IAppInstallService, AppInstallService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IConfigurationService, ConfigurationService>();
            
            #endregion
        }
    }
}