using OverwatchStadiumDatabase.Worker.CrawlerHandlers;
using OverwatchStadiumDatabase.Worker.Services;

namespace OverwatchStadiumDatabase.Worker.DependencyInjection;

public static class CrawlerHandlerExtensions
{
    public static IServiceCollection AddCrawlerHandlers(this IServiceCollection services)
    {
        services.AddScoped<ISqlScriptGenerator, SqlScriptGenerator>();

        var handlerType = typeof(ICrawlerHandler);
        var implementationTypes = AppDomain
            .CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type =>
                handlerType.IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface
            );

        foreach (var implementationType in implementationTypes)
        {
            services.AddKeyedScoped(handlerType, implementationType, implementationType);
        }
        
        services.AddSingleton<CrawlerHandlerManager>();
        
        return services;
    }
}
