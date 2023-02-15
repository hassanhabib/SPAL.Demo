using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Standard.SPAL.Storage.Abstractions;
using Standard.SPAL.Storage.EntityFramework;

namespace Standard.SPAL.Storage.InMemory.Extensions
{
    public static class EntityFrameworkStorageProviderServiceCollectionExtensions
    {
        public static IServiceCollection UseEntityFrameworkStorageProvider(
            this IServiceCollection services,
             Action<DbContextOptionsBuilder>? optionsAction = null)
        {
            services.AddDbContext<EntityFrameworkStorageProvider>(optionsAction);
            services.AddTransient<IStorageProvider, EntityFrameworkStorageProvider>();

            return services;
        }
    }
}
