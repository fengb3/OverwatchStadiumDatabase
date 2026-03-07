using OverwatchStadiumDatabase.Worker.Services;

namespace OverwatchStadiumDatabase.Worker.BackgroundServices;

public class GenerateJsonDataBackgroundService(
    IServiceProvider root,
    ILogger<GenerateJsonDataBackgroundService> logger,
    BackgroundServiceOrchestrator orchestrator
) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            logger.LogInformation("Waiting for crawling to complete...");
            await orchestrator.WaitForAsync<RunCrawlerHandlersBackgroundService>(stoppingToken);
            logger.LogInformation("Crawling completed. Starting JSON data generation...");

            await using var scope = root.CreateAsyncScope();
            var generator = scope.ServiceProvider.GetRequiredService<IJsonDataGenerator>();
            var outputPath = Path.GetFullPath(
                Path.Combine(Directory.GetCurrentDirectory(), "../Data/data.json")
            );
            await generator.GenerateJsonAsync(outputPath, stoppingToken);
            logger.LogInformation("JSON data generation completed: {OutputPath}", outputPath);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while generating JSON data file.");
        }
        finally
        {
            orchestrator.SignalComplete<GenerateJsonDataBackgroundService>();
        }
    }
}
