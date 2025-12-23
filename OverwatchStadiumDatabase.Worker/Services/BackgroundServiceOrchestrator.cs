using System.Collections.Concurrent;

namespace OverwatchStadiumDatabase.Worker.Services;

public class BackgroundServiceOrchestrator
{
    // private readonly TaskCompletionSource _crawlingCompleted = new();
    // private readonly TaskCompletionSource _sqlGenerationCompleted = new();
    //
    // public Task WaitForCrawlingAsync(CancellationToken cancellationToken) =>
    //     _crawlingCompleted.Task.WaitAsync(cancellationToken);
    //
    // public void SignalCrawlingCompleted() => _crawlingCompleted.TrySetResult();
    //
    // public Task WaitForSqlGenerationAsync(CancellationToken cancellationToken) =>
    //     _sqlGenerationCompleted.Task.WaitAsync(cancellationToken);
    //
    // public void SignalSqlGenerationCompleted() => _sqlGenerationCompleted.TrySetResult();

    
    private ConcurrentDictionary<Type, TaskCompletionSource> CompletionSources { get; } = new();
    
    public void SignalComplete<T>() where T : BackgroundService
    {
        if (!CompletionSources.TryGetValue(typeof(T), out var tcs))
        {
            tcs = new TaskCompletionSource();
            CompletionSources[typeof(T)] = tcs;
        }
        
        tcs.TrySetResult();
    }
    
    public Task WaitForAsync<T>(CancellationToken cancellationToken) where T : BackgroundService
    {
        if (!CompletionSources.TryGetValue(typeof(T), out var tcs))
        {
            tcs = new TaskCompletionSource();
            CompletionSources[typeof(T)] = tcs;
        }
        
        return tcs.Task.WaitAsync(cancellationToken);
    }
}
