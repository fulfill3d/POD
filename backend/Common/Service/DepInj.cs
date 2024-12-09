using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using POD.Common.Core.Model;
using POD.Common.Core.Option;
using POD.Common.Database;
using POD.Common.Database.Contexts;
using POD.Common.Database.Models;
using POD.Common.Service.Interfaces;
using SendGrid;
using SendGrid.Extensions.DependencyInjection;

namespace POD.Common.Service
{
    public static class DepInj
    {
        
        public static void AddHttpRequestBodyMapper(this IServiceCollection services)
        {
            services.AddTransient(typeof(IHttpRequestBodyMapper<>), typeof(HttpRequestBodyMapper<>));
        }

        public static void AddFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
        }

        public static void AddFluentValidator<T>(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(T).Assembly);
        }

        public static void AddB2CJwtTokenValidator(
            this IServiceCollection services,
            Action<IServiceProvider, TokenValidationOptions> opt)
        {
            services.ConfigureServiceOptions(opt);
            services.AddSingleton<IJwtValidatorService, JwtValidatorService>();
        }

        public static void AddDatabaseContext<TContext>(
            this IServiceCollection services, DbConnectionOptions dbConnectionsOptions)
            where TContext : DbContext
        {
            
            services.AddDbContext<TContext>(
                options =>
                {
                    if (!options.IsConfigured)
                    {
                        options.UseSqlServer(dbConnectionsOptions.ConnectionString, options =>
                        {
                            options.EnableRetryOnFailure(
                                maxRetryCount: 5, // Number of retry attempts
                                maxRetryDelay: TimeSpan.FromSeconds(30), // Maximum delay between retries
                                errorNumbersToAdd: null); // Add additional SQL error numbers to retry on
                        });
                    }
                });
        }

        public static void ConfigureServiceOptions<TOptions>(
            this IServiceCollection services,
            Action<IServiceProvider, TOptions> configure)
            where TOptions : class
        {
            services
                .AddOptions<TOptions>()
                .Configure<IServiceProvider>((options, resolver) => configure(resolver, options));
        }
        
        public static void AddCommonConfigurationService(
            this IServiceCollection services,
            DbConnectionOptions dbConnectionsOptions)
        {
            services.AddDatabaseContext<DatabaseContext>(dbConnectionsOptions);
            services.AddTransient<ICommonConfigurationService, CommonConfigurationService>();
        }
        
        public static void AddCommonEmailService(
            this IServiceCollection services,
            Action<IServiceProvider, CommonEmailServiceEmailConfigurations> configureCommonEmailService,
            Action<SendGridClientOptions> configureSendGrid)
        {
            services
                .AddOptions<CommonEmailServiceEmailConfigurations>()
                .Configure<IServiceProvider>((options, resolver) => configureCommonEmailService(resolver, options));

            services.AddSendGrid((options) => configureSendGrid(options));

            services.AddTransient<ICommonEmailService, CommonEmailService>();
        }
        
        public static void AddCommonSellerProductService(
            this IServiceCollection services,
            DbConnectionOptions dbConnectionsOptions)
        {
            services.AddDatabaseContext<DatabaseContext>(dbConnectionsOptions);

            services.AddTransient<ICommonSellerProductService, CommonSellerProductService>();
        }

        public static void AddCommonStoreSaleOrderService(
            this IServiceCollection services,
            DbConnectionOptions dbConnectionsOptions)
        {
            services.AddDatabaseContext<DatabaseContext>(dbConnectionsOptions);

            services.AddTransient<ICommonStoreSaleOrderService, CommonStoreSaleOrderService>();
        }

        public static void AddCommonStoreProductService(
            this IServiceCollection services,
            DbConnectionOptions dbConnectionsOptions)
        {
            services.AddDatabaseContext<DatabaseContext>(dbConnectionsOptions);

            services.AddTransient<ICommonStoreProductService, CommonStoreProductService>();
        }

        public static void AddCommonUserService(
            this IServiceCollection services,
            DbConnectionOptions dbConnectionsOptions)
        {
            services.AddDatabaseContext<CommonUserServiceDbContext>(dbConnectionsOptions);

            services.AddTransient<ICommonUserService, CommonUserService>();
        }
    }
}