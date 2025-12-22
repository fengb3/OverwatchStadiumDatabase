using OverwatchStadiumDatabase.Worker.BackgroundServices;
using OverwatchStadiumDatabase.Worker.CrawlerHandlers;
using OverwatchStadiumDatabase.Worker.Services;

namespace OverwatchStadiumDatabase.Worker.DependencyInjection;

public static partial class CrawlerHandlerExtensions
{
    public static IServiceCollection AddCrawlerHandlers(this IServiceCollection services)
    {
        services.AddScoped<ISqlScriptGenerator, SqlScriptGenerator>();

        var handlerType = typeof(ICrawlerHandler);
        var handlers = AppDomain
            .CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type =>
                handlerType.IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface
            );

        foreach (var handler in handlers)
        {
            services.AddScoped(handlerType, handler);
        }
        
        return services;
    }
}
