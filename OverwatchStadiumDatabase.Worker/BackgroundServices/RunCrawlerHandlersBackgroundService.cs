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
        var logger = root.CreateScope()
            .ServiceProvider.GetRequiredService<ILogger<RunCrawlerHandlersBackgroundService>>();
        logger.LogInformation("Starting crawler handlers...");
        var handlers = root.CreateScope().ServiceProvider.GetServices<ICrawlerHandler>().ToList();

        logger.LogInformation("Found {HandlerCount} crawler handlers to execute.", handlers.Count);

        try
        {
            foreach (var handler in handlers)
            {
                await using var scope = root.CreateAsyncScope();

                logger.LogInformation(
                    "Executing crawler handler: {HandlerType}",
                    handler.GetType().Name
                );

                foreach (var url in handler.TargetUrls)
                {
                    logger.LogInformation("Processing URL: {Url}", url);
                    var page = scope.ServiceProvider.GetRequiredService<IPage>();
                    await page.GotoAsync(
                        url,
                        new PageGotoOptions()
                        {
                            Timeout = 200 * 1000, // 200 seconds
                            WaitUntil = WaitUntilState.DOMContentLoaded,
                        }
                    );
                    await handler.HandleAsync(page, stoppingToken);
                    logger.LogInformation("Finished crawler handler for {TargetUrls}", url);
                }
            }

            logger.LogInformation("All crawler handlers have been executed.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while executing crawler handlers.");
        }
        finally
        {
            orchestrator.SignalCrawlingCompleted();
        }
    }
}
