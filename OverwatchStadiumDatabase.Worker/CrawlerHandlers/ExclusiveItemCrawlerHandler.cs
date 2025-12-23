using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Playwright;
using OverwatchStadiumDatabase.Models;

namespace OverwatchStadiumDatabase.Worker.CrawlerHandlers;

/// <summary>
/// 英雄专属物品爬取处理器
/// </summary>
/// <param name="dbContext"></param>
/// <param name="logger"></param>
public class ExclusiveItemCrawlerHandler(
    OverwatchStadiumDbContext dbContext,
    ILogger<ExclusiveItemCrawlerHandler> logger
) : ICrawlerHandler
{

    public async Task HandleAsync(IPage page, CancellationToken cancellationToken)
    {
        // Extract hero name from current URL
        var currentUrl = page.Url;
        var heroName = ExtractHeroNameFromUrl(currentUrl);

        if (string.IsNullOrEmpty(heroName))
        {
            logger.LogWarning("Could not extract hero name from URL: {Url}", currentUrl);
            return;
        }

        logger.LogInformation("Processing exclusive items for hero: {HeroName}", heroName);

        var hero = await dbContext
            .Heroes.Include(h => h.Items)
            .FirstOrDefaultAsync(h => h.Name == heroName, cancellationToken);

        if (hero == null)
        {
            logger.LogWarning("Hero not found in database: {HeroName}", heroName);
            return;
        }

        // Load all buffs
        var allBuffs = await dbContext.Buffs.ToDictionaryAsync(
            b => b.Name,
            b => b,
            cancellationToken
        );

        // All items on a hero's /Stadium page are exclusive to that hero
        var exclusiveItems = await page.QuerySelectorAllAsync("div.ability-details");

        foreach (var itemElement in exclusiveItems)
        {
            // Keep existing behavior: skip blocks without a type.
            var typeElement = await itemElement.QuerySelectorAsync("div.type-block");
            if (typeElement == null)
                continue;

            var header = await itemElement.QuerySelectorAsync("div.header");
            if (header == null)
                continue;

            var itemName = (await header.InnerTextAsync()).Trim();
            logger.LogInformation(
                "Found exclusive item: {ItemName} for {HeroName}",
                itemName,
                hero.Name
            );

            // Check if the item already exists in the database
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
                    Description = string.Empty
                };
                dbContext.Items.Add(item);
            }

            await ItemCrawlerShared.ApplyBasicItemDataAsync(item, itemElement, cancellationToken);
            await ItemCrawlerShared.ReplaceItemBuffsAsync(
                dbContext,
                item,
                itemElement,
                allBuffs,
                cancellationToken
            );

            // Add to hero's items collection if not already there
            // EF Core will automatically manage the HeroExclusive join table
            if (!hero.Items.Contains(item))
            {
                hero.Items.Add(item);
                logger.LogInformation(
                    "Added exclusive item {ItemName} to {HeroName}",
                    item.Name,
                    hero.Name
                );
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        logger.LogInformation(
            "Completed processing exclusive items for hero: {HeroName}",
            heroName
        );
    }

    private static string? ExtractHeroNameFromUrl(string url)
    {
        // URL format: https://overwatch.fandom.com/wiki/{HeroName}/Stadium
        var match = Regex.Match(url, @"/wiki/([^/]+)/Stadium");
        if (match.Success)
        {
            return Uri.UnescapeDataString(match.Groups[1].Value);
        }
        return null;
    }
}
