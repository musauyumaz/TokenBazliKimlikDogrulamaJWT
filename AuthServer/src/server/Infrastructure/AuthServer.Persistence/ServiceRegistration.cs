using AuthServer.Application.Abstractions.Repositories;
using AuthServer.Domain.Entities;
using AuthServer.Persistence.DbContexts;
using AuthServer.Persistence.Repositories.Products;
using AuthServer.Persistence.Repositories.UserRefreshTokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthServer.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuthServerDbContext>(options =>
        {
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            options.UseSqlServer(configuration.GetConnectionString("SqlConnection"));
        });

        services.AddScoped<IReadRepository<Product>, ProductReadRepository>();
        services.AddScoped<IWriteRepository<Product>, ProductWriteRepository>();
        services.AddScoped<IReadRepository<UserRefreshToken>, UserRefreshTokenRepository>();
        services.AddScoped<IWriteRepository<UserRefreshToken>, UserRefreshTokenRepository>();
    }   
}
