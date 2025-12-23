using Microsoft.EntityFrameworkCore;
using OverwatchStadiumDatabase.Worker.Services;

namespace OverwatchStadiumDatabase.Worker.BackgroundServices;

public class MigrateDatabaseBackgroundService(
    IServiceProvider serviceProvider,
    BackgroundServiceOrchestrator orchestrator
    ) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceProvider.CreateScope();

        var logger = scope.ServiceProvider.GetRequiredService<
            ILogger<MigrateDatabaseBackgroundService>
        >();
        logger.LogInformation("Migrating database...");

        var dbContext = scope.ServiceProvider.GetRequiredService<OverwatchStadiumDbContext>();
        dbContext.Database.Migrate();

        logger.LogInformation("Database migration completed.");
        
        orchestrator.SignalComplete<MigrateDatabaseBackgroundService>();
        
        return Task.CompletedTask;
    }
}
