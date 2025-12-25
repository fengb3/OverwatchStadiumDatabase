using Microsoft.Playwright;

namespace OverwatchStadiumDatabase.Worker.DependencyInjection;

public static class Playwright
{
    public static IServiceCollection AddPlaywright(this IServiceCollection services)
    {
        services.AddSingleton<IPlaywright>(_ =>
        {
            var playwright = Microsoft.Playwright.Playwright.CreateAsync().GetAwaiter().GetResult();
            return playwright;
        });

        services.AddSingleton<IBrowser>(provider =>
        {
            var playwright = provider.GetRequiredService<IPlaywright>();
            var isCi = Environment.GetEnvironmentVariable("CI") == "true";
            var browser = playwright
                .Chromium.LaunchAsync(
                    new BrowserTypeLaunchOptions
                    {
                        Headless = isCi,
                        Args = ["--disable-blink-features=AutomationControlled"],
                    }
                )
                .GetAwaiter()
                .GetResult();
            return browser;
        });

        services.AddSingleton<IBrowserContext>(provider =>
        {
            var browser = provider.GetRequiredService<IBrowser>();
            var context = browser
                .NewContextAsync(
                    new BrowserNewContextOptions
                    {
                        UserAgent =
                            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36",
                    }
                )
                .GetAwaiter()
                .GetResult();
            return context;
        });

        services.AddTransient<IPage>(provider =>
        {
            var context = provider.GetRequiredService<IBrowserContext>();
            var page = context.NewPageAsync().GetAwaiter().GetResult();
            return page;
        });

        return services;
    }
}
