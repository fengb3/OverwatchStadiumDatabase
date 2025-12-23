using Microsoft.Playwright;
using OverwatchStadiumDatabase.Worker.CrawlerHandlers;
using OverwatchStadiumDatabase.Worker.DependencyInjection;
using OverwatchStadiumDatabase.Worker.Services;

namespace OverwatchStadiumDatabase.Worker.BackgroundServices;

public partial class RunCrawlerHandlersBackgroundService(
    IServiceProvider root,
    BackgroundServiceOrchestrator orchestrator
) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await orchestrator.WaitForAsync<MigrateDatabaseBackgroundService>(stoppingToken);
        var logger = root.CreateScope()
            .ServiceProvider.GetRequiredService<ILogger<RunCrawlerHandlersBackgroundService>>();
        logger.LogInformation("Starting crawler handlers...");

        try
        {
            var manager = root.CreateScope()
                .ServiceProvider.GetRequiredService<CrawlerHandlerManager>();

            // Register crawler handlers here
            // manager.Register<GeneralItemCrawlerHandler>("https://overwatch.fandom.com/wiki/Stadium/Items");
            manager.Register<HeroCrawlerHandler>(
                "https://overwatch.fandom.com/wiki/Category:Stadium_hero_pages"
            );

            await manager.LoopAsync(stoppingToken);

            logger.LogInformation("Crawler handlers have been registered.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while executing crawler handlers.");
        }
        finally
        {
            orchestrator.SignalComplete<RunCrawlerHandlersBackgroundService>();
        }
    }
}
