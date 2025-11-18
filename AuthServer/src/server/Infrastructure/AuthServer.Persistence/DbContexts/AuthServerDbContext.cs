using AuthServer.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.Persistence.DbContexts
{
    public class AuthServerDbContext(DbContextOptions<AuthServerDbContext> _options) : IdentityDbContext<User,IdentityRole, string>(_options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
    }
}
