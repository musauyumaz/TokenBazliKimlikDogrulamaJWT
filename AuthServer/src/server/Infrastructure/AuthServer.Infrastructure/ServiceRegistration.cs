using AuthServer.Application.Abstractions.Services;
using AuthServer.Infrastructure.DTOs;
using AuthServer.Infrastructure.Services.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace AuthServer.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CustomTokenOption>(configuration.GetSection("TokenOptions"));
        services.Configure<CustomClientOption>(configuration.GetSection("Clients"));

        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
