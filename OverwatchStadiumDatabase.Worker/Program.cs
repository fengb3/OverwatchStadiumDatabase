using OverwatchStadiumDatabase;
using OverwatchStadiumDatabase.Worker.BackgroundServices;
using OverwatchStadiumDatabase.Worker.DependencyInjection;
using OverwatchStadiumDatabase.Worker.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog(configuration =>configuration
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console(
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
