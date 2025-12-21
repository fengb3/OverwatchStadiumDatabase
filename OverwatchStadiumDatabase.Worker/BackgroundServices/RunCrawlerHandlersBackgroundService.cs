using Microsoft.Playwright;
using OverwatchStadiumDatabase.Worker.DependencyInjection;

namespace OverwatchStadiumDatabase.Worker.BackgroundServices;

public class RunCrawlerHandlersBackgroundService(
    IServiceProvider root,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var logger = root.CreateScope().ServiceProvider
            .GetRequiredService<ILogger<RunCrawlerHandlersBackgroundService>>();
        logger.LogInformation("Starting crawler handlers...");
        var handlers = root.CreateScope().ServiceProvider.GetServices<ICrawlerHandler>().ToList();

        logger.LogInformation("Found {HandlerCount} crawler handlers to execute.", handlers.Count);

        try
        {
            foreach (var handler in handlers)
            {
                await using var scope = root.CreateAsyncScope();
                
                logger.LogInformation("Executing crawler handler: {HandlerType}", handler.GetType().Name);
                
                foreach (var url in handler.TargetUrls)
                {
                    logger.LogInformation("Processing URL: {Url}", url);
                    var page = scope.ServiceProvider.GetRequiredService<IPage>();
                    await page.GotoAsync(url, new PageGotoOptions()
                    {
                        Timeout = 60000,
                        WaitUntil = WaitUntilState.DOMContentLoaded
                    });
                    await handler.HandleAsync(page, stoppingToken);
                    logger.LogInformation("Finished crawler handler for {TargetUrls}", url);
                }
            }

            logger.LogInformation("All crawler handlers have been executed.");
        }
        finally
        {
            hostApplicationLifetime.StopApplication();
        }
    }
}