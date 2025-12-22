using Microsoft.Playwright;

namespace OverwatchStadiumDatabase.Worker.CrawlerHandlers;

public interface ICrawlerHandler
{
    public string[] TargetUrls { get; }

    public Task HandleAsync(IPage page, CancellationToken cancellationToken);
}