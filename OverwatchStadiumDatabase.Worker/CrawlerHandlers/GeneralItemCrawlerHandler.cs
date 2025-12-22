using System.Text.RegularExpressions;
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
    public string[] TargetUrls => ["https://overwatch.fandom.com/wiki/Stadium/Items"];

    public async Task HandleAsync(IPage page, CancellationToken cancellationToken)
    {
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
            logger.LogInformation("Processing Item: {ItemName}", itemName);

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
            var typeElement = await itemElement.QuerySelectorAsync("div.type-block");
            if (typeElement != null)
            {
                var typeText = await typeElement.InnerTextAsync();
                item.Type = typeText.Replace("Type", "").Trim();
            }

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

        // await page.ScreenshotAsync(new PageScreenshotOptions
        // {
        //     Path = "standings.png"
        // });

        logger.LogInformation("Screenshot of standings page saved as standings.png");
    }
}
