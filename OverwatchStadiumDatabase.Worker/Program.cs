using OverwatchStadiumDatabase;
using OverwatchStadiumDatabase.Worker;
using OverwatchStadiumDatabase.Worker.BackgroundServices;
using OverwatchStadiumDatabase.Worker.DependencyInjection;
using OverwatchStadiumDatabase.Worker.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog(configuration =>
    configuration.WriteTo.Console(
        theme: Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme.Sixteen
    )
);

builder.Services.AddPlaywright();

builder.Services.AddOverwatchStadiumDatabase();

builder.Services.AddHostedService<MigrateDatabaseBackgroundService>();
builder.Services.AddCrawlerHandlers();

builder.Services.AddSingleton<BackgroundServiceOrchestrator>();

builder.Services.AddHostedService<RunCrawlerHandlersBackgroundService>();
builder.Services.AddHostedService<GenerateSqlScriptBackgroundService>();
builder.Services.AddHostedService<StopApplicationBackgroundService>();

var app = builder.Build();

var exitCode = Microsoft.Playwright.Program.Main(["install", "chromium"]);
if (exitCode != 0)
{
    throw new Exception($"Playwright browser installation failed with exit code {exitCode}");
}

app.Run();
