using OverwatchStadiumDatabase.Models;

namespace OverwatchStadiumDatabase.Calculator.Services;

public sealed class ItemValueCalculator
{
    public sealed record BuffContribution(
        string BuffName,
        decimal Value,
        decimal UnitPrice,
        decimal ContributionValue
    );

    public sealed record ItemValueBreakdown(
        int ItemId,
        string Name,
        string Type,
        string Rarity,
        decimal Cost,
        decimal BaseBuffValue,
        decimal ExtraValue,
        IReadOnlyList<BuffContribution> Contributions
    );

    public sealed record Result(
        IReadOnlyDictionary<string, decimal> UnitPriceByBuffName,
        IReadOnlyList<ItemValueBreakdown> Items
    );

    public Result Compute(IEnumerable<Item> items)
    {
        var materialized = items.Where(i => i.ItemBuffs != null).ToList();

        var commonItems = materialized
            .Where(i => string.Equals(i.Rarity, "Common", StringComparison.OrdinalIgnoreCase))
            .Where(i => i.Cost > 0)
            .ToList();

        var unitPriceByBuff = EstimateUnitPricesFromCommon(commonItems);

        var breakdowns = materialized
            .Select(i => ComputeBreakdown(i, unitPriceByBuff))
            .OrderBy(b => b.Type)
            .ThenBy(b => b.Name)
            .ToList();

        return new Result(unitPriceByBuff, breakdowns);
    }

    private static IReadOnlyDictionary<string, decimal> EstimateUnitPricesFromCommon(
        IReadOnlyList<Item> commonItems
    )
    {
        // We estimate a "unit price" per buff-name based only on Common items.
        // Strategy:
        // 1) If a common item contains exactly one buff, we can directly infer cost/abs(value).
        // 2) For remaining buffs, iteratively allocate the remaining cost on multi-buff items
        //    proportionally to abs(value) and derive implied unit prices.

        var unitPriceByBuff = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);

        // Pass 1: single-buff common items
        var samplesByBuff = new Dictionary<string, List<decimal>>(StringComparer.OrdinalIgnoreCase);
        foreach (var item in commonItems)
        {
            var buffs = GetValidItemBuffs(item).ToList();
            if (buffs.Count != 1)
            {
                continue;
            }

            var ib = buffs[0];
            var name = ib.Buff!.Name;
            var magnitude = Abs(ib.Value);
            if (magnitude <= 0)
            {
                continue;
            }

            var unit = item.Cost / magnitude;
            if (!samplesByBuff.TryGetValue(name, out var list))
            {
                list = new List<decimal>();
                samplesByBuff[name] = list;
            }

            list.Add(unit);
        }

        foreach (var (buffName, samples) in samplesByBuff)
        {
            var median = Median(samples);
            if (median > 0)
            {
                unitPriceByBuff[buffName] = median;
            }
        }

        // Iterative passes: derive implied prices for remaining buffs
        // We cap passes to keep runtime stable in WASM.
        for (var pass = 0; pass < 6; pass++)
        {
            var newSamplesByBuff = new Dictionary<string, List<decimal>>(
                StringComparer.OrdinalIgnoreCase
            );

            foreach (var item in commonItems)
            {
                var buffs = GetValidItemBuffs(item).ToList();
                if (buffs.Count < 2)
                {
                    continue;
                }

                decimal knownValue = 0;
                var unknown = new List<ItemBuff>();

                foreach (var ib in buffs)
                {
                    var buffName = ib.Buff!.Name;
                    var magnitude = Abs(ib.Value);
                    if (magnitude <= 0)
                    {
                        continue;
                    }

                    if (unitPriceByBuff.TryGetValue(buffName, out var unitPrice))
                    {
                        knownValue += magnitude * unitPrice;
                    }
                    else
                    {
                        unknown.Add(ib);
                    }
                }

                if (unknown.Count == 0)
                {
                    continue;
                }

                var remaining = item.Cost - knownValue;
                if (remaining <= 0)
                {
                    continue;
                }

                var totalUnknownMagnitude = unknown.Sum(ib => Abs(ib.Value));
                if (totalUnknownMagnitude <= 0)
                {
                    continue;
                }

                foreach (var ib in unknown)
                {
                    var buffName = ib.Buff!.Name;
                    var magnitude = Abs(ib.Value);
                    if (magnitude <= 0)
                    {
                        continue;
                    }

                    var allocatedCost = remaining * (magnitude / totalUnknownMagnitude);
                    var impliedUnit = allocatedCost / magnitude;

                    if (!newSamplesByBuff.TryGetValue(buffName, out var list))
                    {
                        list = new List<decimal>();
                        newSamplesByBuff[buffName] = list;
                    }

                    list.Add(impliedUnit);
                }
            }

            var changed = false;
            foreach (var (buffName, samples) in newSamplesByBuff)
            {
                if (unitPriceByBuff.ContainsKey(buffName))
                {
                    continue;
                }

                var median = Median(samples);
                if (median > 0)
                {
                    unitPriceByBuff[buffName] = median;
                    changed = true;
                }
            }

            if (!changed)
            {
                break;
            }
        }

        return unitPriceByBuff;
    }

    private static ItemValueBreakdown ComputeBreakdown(
        Item item,
        IReadOnlyDictionary<string, decimal> unitPriceByBuff
    )
    {
        var contributions = new List<BuffContribution>();
        decimal baseBuffValue = 0;

        foreach (var ib in GetValidItemBuffs(item))
        {
            var buffName = ib.Buff!.Name;
            var magnitude = Abs(ib.Value);

            unitPriceByBuff.TryGetValue(buffName, out var unitPrice);
            var contributionValue = magnitude * unitPrice;

            baseBuffValue += contributionValue;

            contributions.Add(
                new BuffContribution(buffName, ib.Value, unitPrice, contributionValue)
            );
        }

        // ExtraValue captures the portion not explained by the baseline "common" unit prices.
        var extraValue = item.Cost - baseBuffValue;

        return new ItemValueBreakdown(
            item.Id,
            item.Name,
            item.Type,
            item.Rarity,
            item.Cost,
            baseBuffValue,
            extraValue,
            contributions
                .OrderByDescending(c => c.ContributionValue)
                .ThenBy(c => c.BuffName)
                .ToList()
        );
    }

    private static IEnumerable<ItemBuff> GetValidItemBuffs(Item item)
    {
        if (item.ItemBuffs == null)
        {
            yield break;
        }

        foreach (var ib in item.ItemBuffs)
        {
            if (ib.Buff == null)
            {
                continue;
            }

            if (ib.Value == 0)
            {
                continue;
            }

            yield return ib;
        }
    }

    private static decimal Median(IReadOnlyList<decimal> values)
    {
        if (values.Count == 0)
        {
            return 0;
        }

        var ordered = values.OrderBy(v => v).ToList();
        var mid = ordered.Count / 2;
        if (ordered.Count % 2 == 1)
        {
            return ordered[mid];
        }

        return (ordered[mid - 1] + ordered[mid]) / 2;
    }

    private static decimal Abs(decimal v) => v < 0 ? -v : v;
}
