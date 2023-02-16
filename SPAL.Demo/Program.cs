using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SPAL.Demo.Brokers;
using SPAL.Demo.Models.Students;
using SPAL.Demo.Services.Foundations.Students;
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
        var studentService = serviceProvider.GetService<IStudentService>();

        Student newStudent = new Student
        {
            Id = Guid.NewGuid(),
            IdentityNumber = Guid.NewGuid().ToString(),
            FirstName = "FirstName",
            LastName = "LastName"
        };

        if (studentService != null)
        {
            await studentService.AddStudentAsync(newStudent);
            Student student = await studentService.RetrieveStudentByIdAsync(newStudent.Id);
            await studentService.RemoveStudentByIdAsync(student.Id);
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
            .AddTransient<IStudentService, StudentService>()
            .AddTransient<IStorageBroker, StorageBroker>()
            .AddTransient<IStorageAbstractProvider, StorageAbstractProvider>()
            .UseInMemoryStorageProvider()
            //.UseEntityFrameworkStorageProvider(options =>
            //    {
            //        options.UseSqlServer(configuration.GetConnectionString(name: "DefaultConnection"));
            //        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            //    })
            .BuildServiceProvider();

        return serviceProvider;
    }
}
