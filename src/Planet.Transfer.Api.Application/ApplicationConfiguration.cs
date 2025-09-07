using Common.Lib.Handler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Planet.Transfer.Api.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.RegisteRequestHandlers(assembly);
        services.RegisterSupplierHttpClients(configuration);
        return services;
    }
    private static IServiceCollection RegisterSupplierHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        ApplicationConstatns.MyTransfer_API_KEY = configuration[$"{nameof(HttpClientsSettings)}:{ApplicationConstatns.MyTransferHttpClientName}:{ApplicationConstatns.PrivateApiKey}"]
                   ?? throw new NullReferenceException("unable to load private ApiKey");

        ApplicationConstatns.GeoApiComplete_API_KEY = configuration[$"{nameof(HttpClientsSettings)}:{ApplicationConstatns.GeoApiCompleteHttpClientName}:{ApplicationConstatns.PrivateApiKey}"]
                   ?? throw new NullReferenceException("unable to load private ApiKey");

        ApplicationConstatns.GeoApiSearch_API_KEY = configuration[$"{nameof(HttpClientsSettings)}:{ApplicationConstatns.GeoApiSearchHttpClientName}:{ApplicationConstatns.PrivateApiKey}"]
                   ?? throw new NullReferenceException("unable to load private ApiKey");

        services.Configure<HttpClientsSettings>(configuration.GetSection(nameof(HttpClientsSettings)));

        HttpClientsSettings httpClientsSettings = configuration
            .GetSection(nameof(HttpClientsSettings))
            .Get<HttpClientsSettings>()
           ?? throw new NullReferenceException("unable to load http client settings");

        foreach (var clientEntry in httpClientsSettings)
        {
            var clientName = clientEntry.Key;
            var settings = clientEntry.Value;

            services.AddHttpClient(clientName, client =>
            {
                client.BaseAddress = new Uri(settings.BaseAddress);
                if (settings.Headers != null)
                {
                    foreach (var header in settings.Headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }

                //if (clientName.StartsWith("GeoApi", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(settings.ApiKey))
                //{
                //    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("ApiKey", settings.ApiKey);
                //}
            });

        }
        return services;
    }
}
