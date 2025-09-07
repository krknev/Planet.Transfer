using Common.Lib.Endpoints;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Planet.Transfer.Api.Web;

public static class ApiConfigurations
{
    public static IServiceCollection AddApiComponents(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();
        services.AddControllers();

        services.AddOpenApi(options =>
        {
            var hostUrlForSwagger = configuration["Swagger:Url"];
            Console.WriteLine("------------------------Swager host url:" + hostUrlForSwagger);
            if (!string.IsNullOrEmpty(hostUrlForSwagger))
            {
                options.AddDocumentTransformer((document, context, cancellationToken) =>
                {
                    document.Servers.Clear();
                    document.Servers.Add(new OpenApiServer
                    {
                        Url = hostUrlForSwagger,
                    });

                    return Task.CompletedTask;
                });
            }
        });
        //   services.AddEndpointsApiExplorer(); 
        services
          .AddEndpoints()
          .AddCorsPolicy();

        return services;
    }
    private static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        ServiceDescriptor[] serviceDescriptors = [.. Assembly.GetCallingAssembly()
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))];

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }
    private static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        => services.AddCors(options =>
        {
            options.AddPolicy("AllowApp",
                policy => policy
                .AllowAnyOrigin()
                                 //                .WithOrigins("http://localhost:4200")
                                 .AllowAnyHeader()
                                 .AllowAnyMethod()
                                 //.AllowCredentials()
                                 );
        });
    private static WebApplication MapEndpoints(this WebApplication app, RouteGroupBuilder? routeGroupBuilder = null)
    {
        var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoint(app);
        }
        return app;
    }

    public static IApplicationBuilder AddApiDevelopmentDocumentaion(this WebApplication app)
    {
        app.UseSwaggerUI(opt => opt.SwaggerEndpoint("/openapi/v1.json", "OpenAPI V1"));
        app.MapOpenApi();

        return app;
    }

    public static WebApplication AddApiBuildConfiguration(this WebApplication app)
    {
        app.UseCors("AllowApp");
        app.MapEndpoints();
        return app;
    }
}
