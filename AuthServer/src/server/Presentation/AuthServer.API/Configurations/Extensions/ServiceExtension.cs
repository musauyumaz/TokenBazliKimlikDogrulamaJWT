using AuthServer.API.Configurations.Transformers;
using AuthServer.Infrastructure.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

    private static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var tokenOptions = configuration.GetSection("TokenOptions").Get<CustomTokenOption>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = tokenOptions?.Issuer,
                ValidAudience = tokenOptions?.Audience?.FirstOrDefault(),
                ValidAudiences = tokenOptions?.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions?.SecurityKey ?? string.Empty)),
                ValidateIssuerSigningKey = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

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
