using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Playwright;
using OverwatchStadiumDatabase.Models;

namespace OverwatchStadiumDatabase.Worker.CrawlerHandlers;

internal static class ItemCrawlerShared
{
    internal sealed record BasicItemData(
        Uri ImageUri,
        string Type,
        string Rarity,
        decimal Cost,
        string Description
    );

    /// <summary>
    /// Extracts the shared/basic fields used by both GeneralItemCrawlerHandler and ExclusiveItemCrawlerHandler.
    /// Note: this method does NOT handle name (header) nor buffs; callers handle those separately.
    /// </summary>
    internal static async Task<BasicItemData> ExtractBasicItemDataAsync(
        IElementHandle itemElement,
        CancellationToken cancellationToken
    )
    {
        cancellationToken.ThrowIfCancellationRequested();

        // Image: wiki markup differs a bit between pages; try both selectors.
        var imgElement =
            await itemElement.QuerySelectorAsync("div.ability-icon > figure > span > img")
            ?? await itemElement.QuerySelectorAsync("div.ability-icon img");

        var imageUri = new Uri("about:blank");
        if (imgElement != null)
        {
            var src = await imgElement.GetAttributeAsync("src");
            if (!string.IsNullOrEmpty(src) && Uri.TryCreate(src, UriKind.Absolute, out var uri))
                imageUri = uri;
        }

        // Type
        var type = string.Empty;
        var typeElement = await itemElement.QuerySelectorAsync("div.type-block");
        if (typeElement != null)
        {
            var typeText = await typeElement.InnerTextAsync();
            type = typeText.Replace("Type", "").Trim();
        }

        // Rarity
        var rarity = string.Empty;
        var rarityElement = await itemElement.QuerySelectorAsync("div.stadium-rarity");
        if (rarityElement != null)
        {
            rarity = (await rarityElement.InnerTextAsync()).Trim();
        }
        if (string.IsNullOrWhiteSpace(rarity))
            rarity = "Common";

        // Cost
        decimal cost = 0;
        var costElement = await itemElement.QuerySelectorAsync("div.stadium-cost b");
        if (costElement != null)
        {
            var costText = (await costElement.InnerTextAsync()).Trim().Replace(",", "");
            decimal.TryParse(costText, NumberStyles.Number, CultureInfo.InvariantCulture, out cost);
        }

        // Description
        var description = string.Empty;
        var descElement = await itemElement.QuerySelectorAsync("div.summary-description");
        if (descElement != null)
        {
            description = (await descElement.InnerTextAsync()).Trim();
        }

        return new BasicItemData(imageUri, type, rarity, cost, description);
    }

    internal static async Task ApplyBasicItemDataAsync(
        Item item,
        IElementHandle itemElement,
        CancellationToken cancellationToken
    )
    {
        var data = await ExtractBasicItemDataAsync(itemElement, cancellationToken);
        item.ImageUri = data.ImageUri;
        item.Type = data.Type;
        item.Rarity = data.Rarity;
        item.Cost = data.Cost;
        item.Description = data.Description;
    }

    internal static async Task ReplaceItemBuffsAsync(
        OverwatchStadiumDbContext dbContext,
        Item item,
        IElementHandle itemElement,
        Dictionary<string, Buff> allBuffs,
        CancellationToken cancellationToken
    )
    {
        cancellationToken.ThrowIfCancellationRequested();

        item.ItemBuffs.Clear();
        var statsRows = await itemElement.QuerySelectorAllAsync("div.stats .data-row");
        foreach (var row in statsRows)
        {
            var valueElement = await row.QuerySelectorAsync(".data-row-value");
            if (valueElement == null)
                continue;

            var text = (await valueElement.InnerTextAsync()).Trim();
            var match = Regex.Match(text, @"^([+\-]?[\d\.,]+%?)\s+(.*)$");
            if (!match.Success)
                continue;

            var valueStr = match.Groups[1].Value.Replace("%", "").Replace("+", "").Replace(",", "");
            var buffName = match.Groups[2].Value.Trim();

            if (!decimal.TryParse(valueStr, NumberStyles.Number, CultureInfo.InvariantCulture, out var value))
                continue;

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
