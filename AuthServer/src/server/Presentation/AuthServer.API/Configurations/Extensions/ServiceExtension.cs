using AuthServer.API.Configurations.Transformers;
using SharedLibrary.Extensions;

namespace AuthServer.API.Configurations.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddAuthenticationServices(configuration);
        services.AddOpenApiServices();

        return services;
    }
    private static IServiceCollection AddOpenApiServices(this IServiceCollection services)
    {
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
        });

        return services;
    }
}
