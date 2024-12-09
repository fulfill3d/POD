using Microsoft.Extensions.DependencyInjection;
using POD.Common.Core.Option;
using POD.Common.Database;
using POD.Common.Service;
using POD.Functions.Payment.Schedule.Data.Database;
using POD.Functions.Payment.Schedule.Services;
using POD.Functions.Payment.Schedule.Services.Interfaces;
using POD.Functions.Payment.Schedule.Services.Mappings;
using POD.Functions.Payment.Schedule.Services.Options;
using POD.Integrations.ServiceBusClient;
using POD.Integrations.ServiceBusClient.Options;
using SendGrid;

namespace POD.Functions.Payment.Schedule
{
    public static class DepInj
    {
        public static void RegisterServices(
            this IServiceCollection services,
            DbConnectionOptions dbConnectionsOptions,
            Action<SendGridClientOptions> configureSendGrid,
            Action<CommonEmailServiceEmailConfigurations> configureEmail,
            Action<ServiceBusClientOptions> configureServiceBus,
            Action<ConfigurationNames> configureConfigurationNames,
            Action<PaymentErrorEmailConfigurations> configurePaymentErrorEmail,
            Action<PaymentProcessingOptions> configurePaymentProcessingOptions,
            Action<QueueNames> configureQueueNames)
        {
            #region Options

            services.ConfigureServiceOptions<ConfigurationNames>((_, options) => configureConfigurationNames(options));
            services.ConfigureServiceOptions<PaymentErrorEmailConfigurations>((_, options) => configurePaymentErrorEmail(options));
            services.ConfigureServiceOptions<PaymentProcessingOptions>((_, options) => configurePaymentProcessingOptions(options));
            services.ConfigureServiceOptions<QueueNames>((_, options) => configureQueueNames(options));

            #endregion

            #region Services
            
            services.AddAutoMapper(typeof(GeneralProfile));
            services.AddCommonEmailService((_, options) => configureEmail(options), configureSendGrid);
            services.AddCommonConfigurationService(dbConnectionsOptions);
            services.AddCommonStoreSaleOrderService(dbConnectionsOptions);
            services.AddCommonUserService(dbConnectionsOptions);
            services.RegisterServiceBusClient(configureServiceBus);
            services.AddDatabaseContext<ScheduleContext>(dbConnectionsOptions);
            services.AddTransient<IScheduleService, ScheduleService>();

            services.AddTransient<IConfigurationService, ConfigurationService>();
            services.AddTransient<IStoreSaleOrderService, StoreSaleOrderService>();
            services.AddTransient<IStoreSaleTransactionService, StoreSaleTransactionService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IPaymentProviderService, PaymentProviderService>();
            services.AddTransient<IServiceBusService, ServiceBusService>();

            #endregion
        }
    }
}