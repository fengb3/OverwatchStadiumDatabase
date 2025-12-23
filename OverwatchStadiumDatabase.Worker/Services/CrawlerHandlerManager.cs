using System.Threading.Channels;
using Microsoft.Playwright;
using OverwatchStadiumDatabase.Worker.CrawlerHandlers;

namespace OverwatchStadiumDatabase.Worker.Services;

public class CrawlerHandlerManager(IServiceProvider root)
{
    private Channel<CrawlerHandlerExecutor> _taskChannel = Channel.CreateUnbounded<CrawlerHandlerExecutor>();

    public void Register<T>(params string[] urls) where T : ICrawlerHandler
    {
        var executors = urls.Select(url => new CrawlerHandlerExecutor<T>(url)).ToList();

        foreach (var executor in executors)
        {
            _taskChannel.Writer.TryWrite(executor);
        }

        // 确保写入完成后关闭 Writer，以便 Reader 知道何时结束（如果这是一次性批处理）
        // 如果是长期运行的服务，可能不需要在这里关闭，而是由外部控制
        // _taskChannel.Writer.TryComplete(); 
    }

    private bool _isRunning = false;

    public async Task LoopAsync(CancellationToken ct = default)
    {
        if (_isRunning)
        {
            return;
        }

        _isRunning = true;

        var logger = root.CreateScope()
            .ServiceProvider.GetRequiredService<ILogger<CrawlerHandlerManager>>();

        var idleTimeout = TimeSpan.FromMinutes(1);
        using var concurrency = new SemaphoreSlim(3, 3);
        var runningTasks = new List<Task>();

        try
        {
            while (!ct.IsCancellationRequested)
            {
                // If nothing is queued and nothing is running, wait up to idleTimeout for new work.
                // If still nothing arrives, we stop the loop.
                var waitToReadTask = _taskChannel.Reader.WaitToReadAsync(ct).AsTask();
                var timeoutTask = Task.Delay(idleTimeout, ct);

                var completed = await Task.WhenAny(waitToReadTask, timeoutTask);
                if (completed == timeoutTask)
                {
                    if (_taskChannel.Reader.Count == 0 && runningTasks.All(t => t.IsCompleted))
                    {
                        logger.LogInformation("CrawlerHandlerManager idle for {IdleTimeout}. Stopping loop.", idleTimeout);
                        break;
                    }

                    // Something is still running; continue waiting.
                    continue;
                }

                // Drain available items quickly.
                while (_taskChannel.Reader.TryRead(out var executor))
                {
                    await concurrency.WaitAsync(ct);

                    // Fire-and-track, but enforce max concurrency via SemaphoreSlim.
                    var task = Task.Run(async () =>
                    {
                        await using var scope = root.CreateAsyncScope();
                        var scopedLogger = scope.ServiceProvider.GetRequiredService<ILogger<CrawlerHandlerManager>>();

                        try
                        {
                            var page = scope.ServiceProvider.GetRequiredService<IPage>();
                            var handler = scope.ServiceProvider.GetRequiredKeyedService<ICrawlerHandler>(executor.HandlerType);

                            scopedLogger.LogInformation("Starting crawler handler for URL: {Url}", executor.Url);

                            await page.GotoAsync(
                                executor.Url,
                                new PageGotoOptions
                                {
                                    Timeout = 200 * 1000, // 200 seconds
                                    WaitUntil = WaitUntilState.DOMContentLoaded,
                                }
                            );

                            await Task.Delay(3000, ct);
                            await handler.HandleAsync(page, ct);
                            await page.CloseAsync();

                            scopedLogger.LogInformation("Completed crawler handler for URL: {Url}", executor.Url);
                        }
                        catch (OperationCanceledException) when (ct.IsCancellationRequested)
                        {
                            // Graceful shutdown.
                        }
                        catch (Exception ex)
                        {
                            scopedLogger.LogError(ex, "Error processing crawler handler for URL: {Url}", executor.Url);
                        }
                        finally
                        {
                            concurrency.Release();
                        }
                    }, ct);

                    runningTasks.Add(task);

                    // Keep the list from growing unbounded.
                    runningTasks.RemoveAll(t => t.IsCompleted);
                }
            }
        }
        finally
        {
            // Wait for whatever we started.
            try
            {
                await Task.WhenAll(runningTasks);
            }
            catch
            {
                // Exceptions are already logged per-task.
            }

            _isRunning = false;
        }
    }
}

public record CrawlerHandlerExecutor(
    Type HandlerType,
    string Url
);

public record CrawlerHandlerExecutor<T>(
    string Url
) : CrawlerHandlerExecutor(typeof(T), Url) where T : ICrawlerHandler;