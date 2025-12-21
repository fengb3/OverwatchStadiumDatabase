using Microsoft.EntityFrameworkCore;
using Microsoft.Playwright;
using OverwatchStadiumDatabase.Models;
using OverwatchStadiumDatabase.Worker.DependencyInjection;

namespace OverwatchStadiumDatabase.Worker.CrawlerHandlers;

public class HeroCrawlerHandler(OverwatchStadiumDbContext dbContext, ILogger<HeroCrawlerHandler> logger) : ICrawlerHandler
{
    public string[] TargetUrls => ["https://overwatch.fandom.com/wiki/Stadium"];

    public async Task HandleAsync(IPage page, CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting Hero crawling");

        // Locate the table following the "Heroes" h2
        var tableLocator = page.Locator("//h2[contains(., 'Heroes')]/following-sibling::table[contains(@class, 'wikitable')][1]");
        
        if (await tableLocator.CountAsync() == 0)
        {
            logger.LogWarning("Could not find Heroes table.");
            return;
        }

        var listItems = tableLocator.Locator("td ul li");
        var count = await listItems.CountAsync();
        
        logger.LogInformation("Found {Count} potential heroes.", count);

        for (int i = 0; i < count; i++)
        {
            var li = listItems.Nth(i);
            
            // The name is usually the text content. 
            // Sometimes there might be an image link first.
            // We can try to get the text from the last anchor tag if it exists, or fallback to li text.
            var anchors = li.Locator("a");
            string name;
            if (await anchors.CountAsync() > 1)
            {
                name = await anchors.Last.InnerTextAsync();
            }
            else
            {
                name = await li.InnerTextAsync();
            }
            
            name = name.Trim();
            
            if (string.IsNullOrWhiteSpace(name)) continue;

            logger.LogInformation("Processing Hero: {HeroName}", name);

            var hero = await dbContext.Heroes.FirstOrDefaultAsync(h => h.Name == name, cancellationToken);
            if (hero == null)
            {
                hero = new Hero { Name = name };
                dbContext.Heroes.Add(hero);
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Hero crawling completed.");
    }
}
