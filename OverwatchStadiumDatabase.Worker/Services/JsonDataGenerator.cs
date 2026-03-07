using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace OverwatchStadiumDatabase.Worker.Services;

public interface IJsonDataGenerator
{
    Task GenerateJsonAsync(string outputPath, CancellationToken cancellationToken);
}

public class JsonDataGenerator(
    OverwatchStadiumDbContext dbContext,
    ILogger<JsonDataGenerator> logger
) : IJsonDataGenerator
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public async Task GenerateJsonAsync(string outputPath, CancellationToken cancellationToken)
    {
        logger.LogInformation("Generating JSON data file to {OutputPath}...", outputPath);

        var heroes = await dbContext
            .Heroes.AsNoTracking()
            .Include(h => h.Items)
            .ThenInclude(i => i.ItemBuffs)
            .ThenInclude(ib => ib.Buff)
            .OrderBy(h => h.Id)
            .ToListAsync(cancellationToken);

        var heroItemIds = await dbContext
            .Heroes.AsNoTracking()
            .SelectMany(h => h.Items.Select(i => i.Id))
            .ToHashSetAsync(cancellationToken);

        var generalItems = await dbContext
            .Items.AsNoTracking()
            .Include(i => i.ItemBuffs)
            .ThenInclude(ib => ib.Buff)
            .Where(i => !heroItemIds.Contains(i.Id))
            .OrderBy(i => i.Id)
            .ToListAsync(cancellationToken);

        var data = new
        {
            generatedAt = DateTime.UtcNow.ToString("O"),
            heroes = heroes.Select(h => new
            {
                h.Id,
                h.Name,
                exclusiveItems = h.Items.OrderBy(i => i.Id).Select(i => MapItem(i)),
            }),
            generalItems = generalItems.Select(i => MapItem(i)),
        };

        var json = JsonSerializer.Serialize(data, SerializerOptions);
        await File.WriteAllTextAsync(outputPath, json, cancellationToken);

        logger.LogInformation("JSON data file generated successfully.");
    }

    private static object MapItem(Models.Item item) =>
        new
        {
            item.Id,
            item.Name,
            ImageUri = item.ImageUri.ToString(),
            item.Type,
            item.Cost,
            item.Rarity,
            item.Description,
            buffs = item.ItemBuffs.OrderBy(ib => ib.Id).Select(ib => new
            {
                name = ib.Buff?.Name,
                ib.Value,
            }),
        };
}
