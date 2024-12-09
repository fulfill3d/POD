using Microsoft.Extensions.DependencyInjection;
using POD.Common.Database;
using POD.Common.Service;
using POD.Functions.PublishSchedule.Data.Database;
using POD.Functions.PublishSchedule.Services;
using POD.Functions.PublishSchedule.Services.Interfaces;
using POD.Functions.PublishSchedule.Services.Options;
using POD.Integrations.ServiceBusClient;
using POD.Integrations.ServiceBusClient.Options;

namespace POD.Functions.PublishSchedule
{
    public static class DepInj
    {
        public static void RegisterServices(
            this IServiceCollection services,
            DbConnectionOptions dbConnectionsOptions,
            Action<ServiceBusClientOptions> configureServiceBusClientOptions,
            Action<PublishScheduleServiceOption> configurePublishProcessingSchedule,
            Action<ServiceBusServiceOption> configureServiceBusServiceOption)
        {
            #region Options

            services.ConfigureServiceOptions<PublishScheduleServiceOption>
                ((_, options) => configurePublishProcessingSchedule(options));
            services.ConfigureServiceOptions<ServiceBusServiceOption>
                ((_, options) => configureServiceBusServiceOption(options));

            #endregion

            #region Services

            services.AddDatabaseContext<PublishScheduleContext>(dbConnectionsOptions);
            services.RegisterServiceBusClient(configureServiceBusClientOptions);
            
            services.AddTransient<ISellerProductService, SellerProductService>();
            services.AddTransient<IServiceBusService, ServiceBusService>();
            services.AddTransient<IPublishScheduleService, PublishScheduleService>();

            #endregion
        }
    }
}