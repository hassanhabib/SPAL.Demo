using Microsoft.Extensions.DependencyInjection;
using Standard.SPAL.Storage.Abstractions;
using Standard.SPAL.Storage.EntityFramework;

namespace Standard.SPAL.Storage.InMemory.Extensions
{
    public static class InMemoryStorageProviderServiceCollectionExtensions
    {
        public static IServiceCollection UseInMemoryStorageProvider(this IServiceCollection services)
        {
            services.AddSingleton<IStorageProvider, InMemoryStorageProvider>();

            return services;
        }
    }
}
