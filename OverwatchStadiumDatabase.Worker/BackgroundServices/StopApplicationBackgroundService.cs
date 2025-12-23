using OverwatchStadiumDatabase.Worker.Services;

namespace OverwatchStadiumDatabase.Worker.BackgroundServices;

public partial class StopApplicationBackgroundService(
    IHostApplicationLifetime hostApplicationLifetime,
    BackgroundServiceOrchestrator orchestrator
) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await orchestrator.WaitForAsync<GenerateSqlScriptBackgroundService>(stoppingToken);
        hostApplicationLifetime.StopApplication();
    }
}
