using Microsoft.Extensions.DependencyInjection;
using POD.API.User.Identity.Data.Database;
using POD.API.User.Identity.Services.Options;
using POD.API.User.Identity.Services;
using POD.API.User.Identity.Services.Interfaces;
using POD.Common.Core.Option;
using POD.Common.Database;
using POD.Common.Service;
using SendGrid;

namespace POD.API.User.Identity
{
    public static class DepInj
    {
        public static void RegisterServices(
            this IServiceCollection services,
            DbConnectionOptions dbConnectionsOptions,
            Action<TokenServiceOption> configureToken,
            Action<IdentityServiceOption> configureIdentity,
            Action<CommonEmailServiceEmailConfigurations> emailService,
            Action<SendGridClientOptions> sendGridClientOptions)
        {
            #region Options

            services.ConfigureServiceOptions<TokenServiceOption>((_, options) => configureToken(options));
            services.ConfigureServiceOptions<IdentityServiceOption>((_, options) => configureIdentity(options));

            #endregion

            #region Services

            services.AddHttpClient();
            services.AddCommonEmailService((_, options) => emailService(options), sendGridClientOptions);
            services.AddDatabaseContext<IdentityContext>(dbConnectionsOptions);
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IEmailService, EmailService>();

            #endregion
        }
    }
}