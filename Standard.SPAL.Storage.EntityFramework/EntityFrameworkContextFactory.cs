using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Standard.SPAL.Storage.EntityFramework
{
    internal class EntityFrameworkContextFactory : IDesignTimeDbContextFactory<EntityFrameworkStorageProvider>
    {
        public EntityFrameworkStorageProvider CreateDbContext(string[] args)
        {
            var environmentName = args.FirstOrDefault() ?? "Development";

            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true);

            IConfiguration configuration = configurationBuilder.Build();

            DbContextOptionsBuilder<EntityFrameworkStorageProvider> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseSqlServer(configuration.GetConnectionString(name: "DefaultConnection"));

            return new EntityFrameworkStorageProvider(dbContextOptionsBuilder.Options);
        }
    }
}
