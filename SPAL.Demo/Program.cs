using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SPAL.Demo.Brokers;
using SPAL.Demo.Models.Students;
using Standard.SPAL.Storage.Abstractions;
using Standard.SPAL.Storage.InMemory.Extensions;

public class Program
{
    static async Task Main(string[] args)
    {
        var environmentName = args.FirstOrDefault() ?? "Development";

        var configurationBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true);

        IConfiguration configuration = configurationBuilder.Build();
        ServiceProvider serviceProvider = ConfigureServices(configuration);
        var broker = serviceProvider.GetService<IStorageBroker>();

        Student newStudent = new Student
        {
            Id = Guid.NewGuid(),
            IdentityNumber = Guid.NewGuid().ToString(),
            FirstName = "FirstName",
            LastName = "LastName"
        };

        if (broker != null)
        {
            await broker.InsertStudentAsync(newStudent);
            Student student = await broker.SelectStudentByIdAsync(newStudent.Id);
            await broker.DeleteStudentAsync(student);
        }
    }

    private static ServiceProvider ConfigureServices(IConfiguration configuration)
    {
        var serviceProvider = new ServiceCollection()
            .AddLogging(builder =>
            {
                builder.AddConsole();
            })
            .AddSingleton(configuration)
            .AddTransient<IStorageBroker, StorageBroker>()
            .AddTransient<IStorageAbstractProvider, StorageAbstractProvider>()
#if DEBUG
            .UseInMemoryStorageProvider()
#endif
#if RELEASE
            .UseEntityFrameworkStorageProvider(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString(name: "DefaultConnection"));
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                })
#endif
            .BuildServiceProvider();

        return serviceProvider;
    }
}
