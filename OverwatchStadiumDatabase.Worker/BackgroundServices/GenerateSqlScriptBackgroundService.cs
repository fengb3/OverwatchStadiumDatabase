using OverwatchStadiumDatabase.Worker.Services;

namespace OverwatchStadiumDatabase.Worker.BackgroundServices;

public class GenerateSqlScriptBackgroundService(
    IServiceProvider root,
    ILogger<GenerateSqlScriptBackgroundService> logger,
    BackgroundServiceOrchestrator orchestrator
) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            logger.LogInformation("Waiting for crawling to complete...");
            await orchestrator.WaitForAsync<RunCrawlerHandlersBackgroundService>(stoppingToken);
            logger.LogInformation("Crawling completed. Starting SQL script generation...");

            await using var scriptScope = root.CreateAsyncScope();
            var scriptGenerator =
                scriptScope.ServiceProvider.GetRequiredService<ISqlScriptGenerator>();
            var outputPath = Path.GetFullPath(
                Path.Combine(Directory.GetCurrentDirectory(), "../Data/full_data_dump.sql")
            );
            await scriptGenerator.GenerateScriptAsync(outputPath, stoppingToken);
            logger.LogInformation("SQL script generation completed: {OutputPath}", outputPath);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while generating SQL script.");
        }
        finally
        {
            orchestrator.SignalComplete<GenerateSqlScriptBackgroundService>();
        }
    }
}
