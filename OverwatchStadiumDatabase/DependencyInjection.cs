using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OverwatchStadiumDatabase;

public static class DependencyInjection
{
    public static void ConfigureOverwatchStadiumDatabase(this DbContextOptionsBuilder options, string connectionString = "Data Source=../Data/overwatch_stadium.db")
    {
        options.UseSqlite(connectionString);
    }

    public static IServiceCollection AddOverwatchStadiumDatabase(this IServiceCollection services, string connectionString = "Data Source=../Data/overwatch_stadium.db")
    {
        services.AddDbContext<OverwatchStadiumDbContext>(options => options.ConfigureOverwatchStadiumDatabase(connectionString));

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
