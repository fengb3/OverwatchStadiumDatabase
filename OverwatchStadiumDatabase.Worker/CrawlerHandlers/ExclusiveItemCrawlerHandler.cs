using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Playwright;
using OverwatchStadiumDatabase.Models;
using OverwatchStadiumDatabase.Worker.DependencyInjection;

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
    // private string[]? _targetUrls;

    // public string[] TargetUrls
    // {
    //     get
    //     {
    //         if (_targetUrls == null)
    //         {
    //             var heroes = dbContext.Heroes.ToList();
    //             _targetUrls = heroes
    //                 .Select(h =>
    //                     $"https://overwatch.fandom.com/wiki/{Uri.EscapeDataString(h.Name.Replace(" ", "_"))}/Stadium"
    //                 )
    //                 .ToArray();
    //         }
    //         return _targetUrls;
    //     }
    // }

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
            var typeElement = await itemElement.QuerySelectorAsync("div.type-block");
            if (typeElement == null)
                continue;

            var typeText = await typeElement.InnerTextAsync();

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
                item = new Item { Name = itemName, ItemBuffs = new List<ItemBuff>() };
                dbContext.Items.Add(item);
            }

            // Image
            var imgElement = await itemElement.QuerySelectorAsync("div.ability-icon img");
            if (imgElement != null)
            {
                var src = await imgElement.GetAttributeAsync("src");
                if (!string.IsNullOrEmpty(src) && Uri.TryCreate(src, UriKind.Absolute, out var uri))
                {
                    item.ImageUri = uri;
                }
            }

            // Type
            item.Type = typeText.Replace("Type", "").Trim();

            // Rarity
            var rarityElement = await itemElement.QuerySelectorAsync("div.stadium-rarity");
            if (rarityElement != null)
            {
                item.Rarity = (await rarityElement.InnerTextAsync()).Trim();
            }

            if (string.IsNullOrEmpty(item.Rarity))
            {
                item.Rarity = "Common";
            }

            // Cost
            var costElement = await itemElement.QuerySelectorAsync("div.stadium-cost b");
            if (costElement != null)
            {
                var costText = await costElement.InnerTextAsync();
                if (decimal.TryParse(costText.Replace(",", ""), out var cost))
                {
                    item.Cost = cost;
                }
            }

            // Description
            var descElement = await itemElement.QuerySelectorAsync("div.summary-description");
            if (descElement != null)
            {
                item.Description = (await descElement.InnerTextAsync()).Trim();
            }

            // Buffs
            item.ItemBuffs.Clear();
            var statsRows = await itemElement.QuerySelectorAllAsync("div.stats .data-row");
            foreach (var row in statsRows)
            {
                var valueElement = await row.QuerySelectorAsync(".data-row-value");
                if (valueElement != null)
                {
                    var text = (await valueElement.InnerTextAsync()).Trim();
                    // Regex to match number (with optional %, +, -) and text
                    var match = Regex.Match(text, @"^([+\-]?[\d\.,]+%?)\s+(.*)$");
                    if (match.Success)
                    {
                        var valueStr = match.Groups[1].Value.Replace("%", "").Replace("+", "");
                        var buffName = match.Groups[2].Value.Trim();

                        if (decimal.TryParse(valueStr, out var value))
                        {
                            if (!allBuffs.TryGetValue(buffName, out var buff))
                            {
                                buff = new Buff { Name = buffName };
                                dbContext.Buffs.Add(buff);
                                allBuffs[buffName] = buff;
                            }

                            item.ItemBuffs.Add(new ItemBuff { Buff = buff, Value = value });
                        }
                    }
                }
            }

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
