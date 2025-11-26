using AuthServer.Domain.Entities;
using AuthServer.Domain.Entities.Commons;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.Persistence.DbContexts
{
    public sealed class AuthServerDbContext(DbContextOptions<AuthServerDbContext> _options) : IdentityDbContext<User,IdentityRole, string>(_options)
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity is ICreated created)
                        created.CreatedDate = now;
                    if (entry.Entity is IIsActive active)
                        active.IsActive = true;
                }
                else if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity is IUpdated updated)
                        updated.UpdatedDate = now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
