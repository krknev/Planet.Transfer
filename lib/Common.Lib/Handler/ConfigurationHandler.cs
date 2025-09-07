using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Common.Lib.Handler;
public static class ConfigurationHandler
{
    public static IServiceCollection RegisteRequestHandlers(this IServiceCollection services, Assembly assembly)
    {
        services.AddScoped<IMediator, Mediator>();

        var handlerInterfaceTypes = new[]
        {
        typeof(IRequestHandler<>),
        typeof(IRequestHandler<,>)
    };

        var handlerTypes = assembly.GetTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .Select(t => new
            {
                Implementation = t,
                Interfaces = t.GetInterfaces()
                    .Where(i => i.IsGenericType &&
                                handlerInterfaceTypes.Contains(i.GetGenericTypeDefinition()))
                    .ToList()
            })
            .Where(x => x.Interfaces.Any())
            .ToList();

        foreach (var handler in handlerTypes)
        {
            foreach (var @interface in handler.Interfaces)
            {
                services.AddScoped(@interface, handler.Implementation);
            }
        }

        return services;
    }

}
