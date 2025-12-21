using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OverwatchStadiumDatabase;

public static class DependencyInjection
{
    public static void ConfigureOverwatchStadiumDatabase(
        this DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source=overwatch_stadium.db");
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