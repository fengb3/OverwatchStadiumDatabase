using Microsoft.Playwright;

namespace OverwatchStadiumDatabase.Worker.DependencyInjection;

public interface  ICrawlerHandler
{
    public string[] TargetUrls { get; }

    public Task HandleAsync(IPage page, CancellationToken cancellationToken);
}