using POD.Integrations.BlobClient.Interface;
using POD.Integrations.BlobClient.Options;
using Microsoft.Extensions.DependencyInjection;

namespace POD.Integrations.BlobClient
{
    public static class DepInj
    {
        private static void RegisterBlobClient(
            this IServiceCollection services,
            Action<IServiceProvider, BlobClientConfiguration> configureBlobClientOptions)
        {
            services
                .AddOptions<BlobClientConfiguration>()
                .Configure<IServiceProvider>((options, resolver) => configureBlobClientOptions(resolver, options));

            services.AddTransient<IBlobClient, BlobClient>();
        }

        public static void RegisterBlobClient(
            this IServiceCollection services,
            Action<BlobClientConfiguration> configureBlobClientOptions)
        {
            services.RegisterBlobClient(
                (_, options) => configureBlobClientOptions(options));
        }
    }
}