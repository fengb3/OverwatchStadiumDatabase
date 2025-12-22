namespace OverwatchStadiumDatabase.Worker.Services;

public class BackgroundServiceOrchestrator
{
    private readonly TaskCompletionSource _crawlingCompleted = new();
    private readonly TaskCompletionSource _sqlGenerationCompleted = new();

    public Task WaitForCrawlingAsync(CancellationToken cancellationToken) =>
        _crawlingCompleted.Task.WaitAsync(cancellationToken);

    public void SignalCrawlingCompleted() => _crawlingCompleted.TrySetResult();

    public Task WaitForSqlGenerationAsync(CancellationToken cancellationToken) =>
        _sqlGenerationCompleted.Task.WaitAsync(cancellationToken);

    public void SignalSqlGenerationCompleted() => _sqlGenerationCompleted.TrySetResult();
}
