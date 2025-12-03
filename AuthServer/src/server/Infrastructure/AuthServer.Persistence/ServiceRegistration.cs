using AuthServer.Application.Abstractions.Repositories;
using AuthServer.Domain.Entities;
using AuthServer.Persistence.DbContexts;
using AuthServer.Persistence.Repositories.Products;
using AuthServer.Persistence.Repositories.UserRefreshTokens;
using Microsoft.AspNetCore.Identity;
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
        services.AddIdentityCore<User>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;

            options.User.RequireUniqueEmail = true;

            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<AuthServerDbContext>();

    
        services.AddScoped<IReadRepository<Product>, ProductReadRepository>();
        services.AddScoped<IWriteRepository<Product>, ProductWriteRepository>();
        services.AddScoped<IReadRepository<UserRefreshToken>, UserRefreshTokenRepository>();
        services.AddScoped<IWriteRepository<UserRefreshToken>, UserRefreshTokenRepository>();
    }   
}
