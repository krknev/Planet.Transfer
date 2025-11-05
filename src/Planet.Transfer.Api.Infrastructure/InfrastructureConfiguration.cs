using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Planet.Transfer.Api.Application.CQRS.Auth;
using Planet.Transfer.Api.Domain.BoundedContext.Identity;
using Planet.Transfer.Api.Infrastructure.BoundedContext.Identity;

namespace Planet.Transfer.Api.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
         => services
             .AddIdentityDatabase(configuration)
     ;

    private static IServiceCollection AddIdentityDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext<IdentityApplicationDbContext>(options => options
                     .UseNpgsql(
                         configuration.GetConnectionString("IdentityConnection"),
                         sqlServer => sqlServer.MigrationsAssembly(typeof(IdentityApplicationDbContext).Assembly.FullName)));

        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireDigit = true;
            options.SignIn.RequireConfirmedAccount = false;
        })
         .AddEntityFrameworkStores<IdentityApplicationDbContext>()
         .AddDefaultTokenProviders()
         ;
        services
         .AddTransient<IIdentityService, IdentityService>()
         ;
        return services;
    }
}
