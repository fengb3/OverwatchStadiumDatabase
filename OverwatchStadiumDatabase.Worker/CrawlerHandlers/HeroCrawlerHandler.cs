using Microsoft.EntityFrameworkCore;
using Microsoft.Playwright;
using OverwatchStadiumDatabase.Models;
using OverwatchStadiumDatabase.Worker.DependencyInjection;
using OverwatchStadiumDatabase.Worker.Services;

namespace OverwatchStadiumDatabase.Worker.CrawlerHandlers;

/// <summary>
/// 英雄爬取处理器
/// </summary>
/// <param name="dbContext"></param>
/// <param name="logger"></param>
public class HeroCrawlerHandler(
    OverwatchStadiumDbContext dbContext,
    CrawlerHandlerManager crawlerHandlerManager,
    ILogger<HeroCrawlerHandler> logger
) : ICrawlerHandler
{
    // public string[] TargetUrls => ["https://overwatch.fandom.com/wiki/Stadium"];

    public async Task HandleAsync(IPage page, CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting Hero crawling");

        //get links under `#mw-pages > div > div`

        var memberLinks = page.Locator("#mw-pages > div > div a");
        var count = await memberLinks.CountAsync();

        if (count == 0)
        {
            logger.LogWarning("Could not find any hero links on the category page.");
            return;
        }

        logger.LogInformation("Found {Count} potential heroes.", count);

        // count = 6; // for testing, limit to first 6 heroes

        var urls = new List<string>();

        for (int i = 0; i < count; i++)
        {
            var link = memberLinks.Nth(i);
            var name = await link.InnerTextAsync();
            var url = await link.GetAttributeAsync("href");
            if (string.IsNullOrWhiteSpace(url))
                continue;

            urls.Add("https://overwatch.fandom.com" + url);

            name = name.Trim();

            // remove tailing `/Stadium` if exists
            if (name.EndsWith("/Stadium"))
            {
                name = name.Substring(0, name.Length - "/Stadium".Length).Trim();
            }

            if (string.IsNullOrWhiteSpace(name))
                continue;

            logger.LogInformation("Processing Hero: {HeroName}, Url: {Url}", name, url);

            // crawlerHandlerManager.Register<ExclusiveItemCrawlerHandler>(
            //     "https://overwatch.fandom.com" + url
            // );

            var hero = await dbContext.Heroes.FirstOrDefaultAsync(
                h => h.Name == name,
                cancellationToken
            );
            if (hero == null)
            {
                hero = new Hero { Name = name };
                dbContext.Heroes.Add(hero);
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Hero crawling completed.");


        var 从 = 0;
        foreach (var url in urls)
        {
            从++;
            if(从 > 5) break;
            
            logger.LogInformation("Registering ExclusiveItemCrawlerHandler for URL: {Url}", url);
            crawlerHandlerManager.Register<ExclusiveItemCrawlerHandler>(url);
        }

        logger.LogInformation("Registering GeneralItemCrawlerHandler for general items page.");
        crawlerHandlerManager.Register<GeneralItemCrawlerHandler>(
            "https://overwatch.fandom.com/wiki/Stadium/Items"
        );
    }
}
