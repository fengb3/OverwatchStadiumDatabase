using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace OverwatchStadiumDatabase;

public static class DependencyInjection
{
    public static void ConfigureOverwatchStadiumDatabase(this DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source=../Data/overwatch_stadium.db");

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
