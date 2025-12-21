// using System.Text.RegularExpressions;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Playwright;
// using OverwatchStadiumDatabase.Models;
//
// namespace OverwatchStadiumDatabase.Worker.BackgroundServices;
//
// public class FetchDataBackgroundService(IServiceProvider serviceProvider, IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
// {
//     protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//     {
//         var logger = serviceProvider.GetRequiredService<ILogger<FetchDataBackgroundService>>();
//         try
//         {
//             await using var scope = serviceProvider.CreateAsyncScope();
//             await using var dbContext = scope.ServiceProvider.GetRequiredService<OverwatchStadiumDbContext>();
//             
//             var page = scope.ServiceProvider.GetRequiredService<IPage>();
//             await page.GotoAsync("https://overwatch.fandom.com/wiki/Stadium/Items", new PageGotoOptions
//             {
//                 Timeout = 60000,
//                 WaitUntil = WaitUntilState.DOMContentLoaded
//             });
//             
//             var element = await page.QuerySelectorAllAsync("div.ability-details");
//             
//             foreach (var itemElement in element)
//             {
//                 var header = await itemElement.QuerySelectorAsync("div.header");
//                 if (header == null) continue;
//                 var itemName = (await header.InnerTextAsync()).Trim();
//                 logger.LogInformation("Processing Item: {ItemName}", itemName);
//
//                 var item = await dbContext.Items
//                     .Include(i => i.Buffs)
//                     .FirstOrDefaultAsync(i => i.Name == itemName, stoppingToken);
//
//                 if (item == null)
//                 {
//                     item = new Item { Name = itemName, Buffs = new List<Buff>() };
//                     dbContext.Items.Add(item);
//                 }
//
//                 // Image
//                 var imgElement = await itemElement.QuerySelectorAsync("div.ability-icon img");
//                 if (imgElement != null)
//                 {
//                     var src = await imgElement.GetAttributeAsync("src");
//                     if (!string.IsNullOrEmpty(src) && Uri.TryCreate(src, UriKind.Absolute, out var uri))
//                     {
//                         item.ImageUri = uri;
//                     }
//                 }
//
//                 // Type
//                 var typeElement = await itemElement.QuerySelectorAsync("div.type-block");
//                 if (typeElement != null)
//                 {
//                     var typeText = await typeElement.InnerTextAsync();
//                     item.Type = typeText.Replace("Type", "").Trim();
//                 }
//
//                 // Rarity
//                 var rarityElement = await itemElement.QuerySelectorAsync("div.stadium-rarity");
//                 if (rarityElement != null)
//                 {
//                     item.Rarity = (await rarityElement.InnerTextAsync()).Trim();
//                 }
//
//                 // Cost
//                 var costElement = await itemElement.QuerySelectorAsync("div.stadium-cost b");
//                 if (costElement != null)
//                 {
//                     var costText = await costElement.InnerTextAsync();
//                     if (decimal.TryParse(costText.Replace(",", ""), out var cost))
//                     {
//                         item.Cost = cost;
//                     }
//                 }
//
//                 // Description
//                 var descElement = await itemElement.QuerySelectorAsync("div.summary-description");
//                 if (descElement != null)
//                 {
//                     item.Description = (await descElement.InnerTextAsync()).Trim();
//                 }
//
//                 // Buffs
//                 item.Buffs.Clear();
//                 var statsRows = await itemElement.QuerySelectorAllAsync("div.stats .data-row");
//                 foreach (var row in statsRows)
//                 {
//                     var valueElement = await row.QuerySelectorAsync(".data-row-value");
//                     if (valueElement != null)
//                     {
//                         var text = (await valueElement.InnerTextAsync()).Trim();
//                         // Regex to match number (with optional %, +, -) and text
//                         var match = Regex.Match(text, @"^([+\-]?[\d\.,]+%?)\s+(.*)$");
//                         if (match.Success)
//                         {
//                             var valueStr = match.Groups[1].Value.Replace("%", "").Replace("+", "");
//                             var buffName = match.Groups[2].Value.Trim();
//
//                             if (decimal.TryParse(valueStr, out var value))
//                             {
//                                 item.Buffs.Add(new Buff
//                                 {
//                                     BuffName = buffName,
//                                     Value = value
//                                 });
//                             }
//                         }
//                     }
//                 }
//             }
//
//             await dbContext.SaveChangesAsync(stoppingToken);
//             
//             // await page.ScreenshotAsync(new PageScreenshotOptions
//             // {
//             //     Path = "standings.png"
//             // });
//             
//             logger.LogInformation("Screenshot of standings page saved as standings.png");
//         }
//         catch (Exception ex)
//         {
//             logger.LogError(ex, "An error occurred while fetching data.");
//             throw;
//         }
//         finally
//         {
//             hostApplicationLifetime.StopApplication();
//         }
//     }
// }