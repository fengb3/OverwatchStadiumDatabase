using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Playwright;
using OverwatchStadiumDatabase.Models;

namespace OverwatchStadiumDatabase.Worker.CrawlerHandlers;

internal static class ItemCrawlerShared
{
    internal sealed record BasicItemData(
        Uri ImageUri,
        string? ImageSrc,
        string ImageResolution,
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
        Uri? baseUri,
        CancellationToken cancellationToken
    )
    {
        cancellationToken.ThrowIfCancellationRequested();

        // Image: wiki markup differs a bit between pages; try both selectors.
        var imgElement =
            await itemElement.QuerySelectorAsync("div.ability-icon > figure > span > img")
            ?? await itemElement.QuerySelectorAsync("div.ability-icon img");

        var imageUri = new Uri("about:blank");
        string? imageSrc = null;
        var imageResolution = "missing-img-element";
        if (imgElement != null)
        {
            imageSrc =
                await imgElement.GetAttributeAsync("data-src")
                ?? await imgElement.GetAttributeAsync("src");

            if (!string.IsNullOrWhiteSpace(imageSrc))
            {
                imageSrc = imageSrc.Trim();

                if (Uri.TryCreate(imageSrc, UriKind.Absolute, out var absolute))
                {
                    imageUri = absolute;
                    imageResolution = "absolute";
                }
                else if (baseUri != null && imageSrc.StartsWith("//", StringComparison.Ordinal))
                {
                    // Protocol-relative URL (e.g. //static.wikia.nocookie.net/...)
                    if (
                        Uri.TryCreate(
                            $"{baseUri.Scheme}:{imageSrc}",
                            UriKind.Absolute,
                            out var protocolRelative
                        )
                    )
                    {
                        imageUri = protocolRelative;
                        imageResolution = "protocol-relative";
                    }
                    else
                    {
                        imageResolution = "unresolved";
                    }
                }
                else if (baseUri != null && Uri.TryCreate(baseUri, imageSrc, out var resolved))
                {
                    // Relative URL (e.g. /path/to/image.png)
                    imageUri = resolved;
                    imageResolution = "relative";
                }
                else
                {
                    imageResolution = "unresolved";
                }
            }
            else
            {
                imageResolution = "missing-src";
            }
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

        return new BasicItemData(
            imageUri,
            imageSrc,
            imageResolution,
            type,
            rarity,
            cost,
            description
        );
    }

    internal static async Task ApplyBasicItemDataAsync(
        Item item,
        IElementHandle itemElement,
        Uri? baseUri,
        CancellationToken cancellationToken
    )
    {
        var data = await ExtractBasicItemDataAsync(itemElement, baseUri, cancellationToken);
        item.ImageUri = data.ImageUri;
        item.Type = data.Type;
        item.Rarity = data.Rarity;
        item.Cost = data.Cost;
        item.Description = data.Description;
    }

    internal static async Task ApplyBasicItemDataAsync(
        Item item,
        IElementHandle itemElement,
        Uri? baseUri,
        ILogger logger,
        string itemName,
        CancellationToken cancellationToken
    )
    {
        var data = await ExtractBasicItemDataAsync(itemElement, baseUri, cancellationToken);

        item.ImageUri = data.ImageUri;
        item.Type = data.Type;
        item.Rarity = data.Rarity;
        item.Cost = data.Cost;
        item.Description = data.Description;

        // if (item.ImageUri.ToString() == "about:blank")
        // {
        //     logger.LogWarning(
        //         "Item image unresolved: {ItemName} src={Src} base={BaseUrl} resolution={Resolution} imageUri={ImageUri}",
        //         itemName,
        //         data.ImageSrc,
        //         baseUri?.ToString(),
        //         data.ImageResolution,
        //         item.ImageUri.ToString()
        //     );
        // }
        // else
        // {
        //     logger.LogInformation(
        //         "Item image parsed: {ItemName} src={Src} base={BaseUrl} resolution={Resolution} imageUri={ImageUri}",
        //         itemName,
        //         data.ImageSrc,
        //         baseUri?.ToString(),
        //         data.ImageResolution,
        //         item.ImageUri.ToString()
        //     );
        // }
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

            if (
                !decimal.TryParse(
                    valueStr,
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out var value
                )
            )
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
