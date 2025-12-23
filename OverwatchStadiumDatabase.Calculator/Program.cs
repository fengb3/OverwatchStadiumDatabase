using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OverwatchStadiumDatabase;
using OverwatchStadiumDatabase.Calculator;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOverwatchStadiumDatabase("Data Source=overwatch_stadium.db");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var host = builder.Build();

// Download the database file
var httpClient = host.Services.GetRequiredService<HttpClient>();
// Use the current date as the version to cache the database for one day
var version = DateTime.UtcNow.ToString("yyyyMMdd");
var dbBytes = await httpClient.GetByteArrayAsync($"data/overwatch_stadium.db?v={version}");
await File.WriteAllBytesAsync("overwatch_stadium.db", dbBytes);

await host.RunAsync();