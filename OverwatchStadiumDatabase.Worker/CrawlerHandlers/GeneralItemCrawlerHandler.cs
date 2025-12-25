using Microsoft.EntityFrameworkCore;
using Microsoft.Playwright;
using OverwatchStadiumDatabase.Models;
using OverwatchStadiumDatabase.Worker.DependencyInjection;

namespace OverwatchStadiumDatabase.Worker.CrawlerHandlers;

/// <summary>
/// 通用物品爬取处理器
/// </summary>
/// <param name="dbContext"></param>
/// <param name="logger"></param>
public class GeneralItemCrawlerHandler(
    OverwatchStadiumDbContext dbContext,
    ILogger<GeneralItemCrawlerHandler> logger
) : ICrawlerHandler
{
    public async Task HandleAsync(IPage page, CancellationToken cancellationToken)
    {
        Uri? baseUri = null;
        if (
            !string.IsNullOrWhiteSpace(page.Url)
            && Uri.TryCreate(page.Url, UriKind.Absolute, out var parsedBaseUri)
        )
            baseUri = parsedBaseUri;

        // Fetch all heroes to link general items to them
        var heroes = await dbContext.Heroes.Include(h => h.Items).ToListAsync(cancellationToken);
        logger.LogInformation("Found {Count} heroes to link general items to.", heroes.Count);

        // Load all buffs
        var allBuffs = await dbContext.Buffs.ToDictionaryAsync(
            b => b.Name,
            b => b,
            cancellationToken
        );

        var element = await page.QuerySelectorAllAsync("div.ability-details");

        foreach (var itemElement in element)
        {
            var header = await itemElement.QuerySelectorAsync("div.header");
            if (header == null)
                continue;
            var itemName = (await header.InnerTextAsync()).Trim();
            logger.LogInformation("Processing General Item: {ItemName}", itemName);

            var item = await dbContext
                .Items.Include(i => i.ItemBuffs)
                .ThenInclude(ib => ib.Buff)
                .FirstOrDefaultAsync(i => i.Name == itemName, cancellationToken);

            if (item == null)
            {
                item = new Item
                {
                    Name = itemName,
                    ItemBuffs = new List<ItemBuff>(),
                    ImageUri = new Uri("about:blank"),
                    Type = string.Empty,
                    Rarity = string.Empty,
                    Description = string.Empty,
                };
                dbContext.Items.Add(item);
            }

            await ItemCrawlerShared.ApplyBasicItemDataAsync(
                item,
                itemElement,
                baseUri,
                logger,
                itemName,
                cancellationToken
            );
            await ItemCrawlerShared.ReplaceItemBuffsAsync(
                dbContext,
                item,
                itemElement,
                allBuffs,
                cancellationToken
            );

            // Link general item to all heroes
            foreach (var hero in heroes)
            {
                if (!hero.Items.Contains(item))
                {
                    hero.Items.Add(item);
                }
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
