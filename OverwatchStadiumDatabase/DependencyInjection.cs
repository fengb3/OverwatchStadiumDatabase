using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace OverwatchStadiumDatabase;

public static class DependencyInjection
{
    public static void ConfigureOverwatchStadiumDatabase(this DbContextOptionsBuilder options)
    {
        // In CI the working directory may not be the repo root.
        // Resolve the db file relative to the app base directory:
        //   <repo>/OverwatchStadiumDatabase.Worker/bin/<cfg>/<tfm>/  ->  <repo>/Data/overwatch_stadium.db
        var dbPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..", "Data", "overwatch_stadium.db"));
        // var dbPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..", "Data", "overwatch_stadium.db"));

        options.UseSqlite($"Data Source={dbPath}");

        // Keep EF Core logging quiet by default.
        // (If you want SQL for debugging, switch this to LogLevel.Information temporarily.)
        options.LogTo(_ => { }, Microsoft.Extensions.Logging.LogLevel.Warning);
        options.EnableSensitiveDataLogging(false);
        options.EnableDetailedErrors(false);
    }

    public static IServiceCollection AddOverwatchStadiumDatabase(this IServiceCollection services)
    {
        services.AddDbContext<OverwatchStadiumDbContext>(ConfigureOverwatchStadiumDatabase);

        return services;
    }

    // public static WebApplication UseOverwatchStadiumDatabase(this WebApplication app)
    // {
    //     using var scope = app.Services.CreateScope();
    //     var dbContext = scope.ServiceProvider.GetRequiredService<OverwatchStadiumDbContext>();
    //     dbContext.Database.Migrate();
    //
    //     return app;
    // }
}
