using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using smarthome.shared;

// Add console logging
var loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .AddFilter("Microsoft", LogLevel.Warning)
        .AddFilter("System", LogLevel.Warning)
        .AddFilter("smarthome.migrations.Program", LogLevel.Debug)
        .AddConsole();
});

// Read configuration from appsettings.json and environment variables
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

// Create host for EF
var hostBuilder = Host.CreateDefaultBuilder(args);
hostBuilder.ConfigureServices((hostContext, services) =>
{
    // Add database contexts
    services.AddDbContext<AppDatabaseContext>(options =>
    {
        var connectionString = configuration.GetValue<string>("ConnectionString");
        options.UseNpgsql(connectionString);
    });
});
var host = hostBuilder.Build();

// Migrate database function
async Task Migrate(ILogger<Program> logger)
{
    logger?.LogInformation("Migrating database...");

    using (var serviceScope = host.Services.CreateScope())
    {
        var serviceProvider = serviceScope.ServiceProvider;

        try
        {
            var databaseContext = serviceProvider.GetRequiredService<AppDatabaseContext>();

            await databaseContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger?.LogError($"An error occurred while migrating, {ex}");
        }
    }
}

// Run migration
await Migrate(host.Services.GetRequiredService<ILogger<Program>>());