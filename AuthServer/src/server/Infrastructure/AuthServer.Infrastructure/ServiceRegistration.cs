using AuthServer.Infrastructure.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace AuthServer.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CustomTokenOption>(configuration.GetSection("TokenOptions"));
        return services;
    }
}
