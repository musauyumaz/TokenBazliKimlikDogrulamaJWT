using AuthServer.Application.Abstractions.Repositories;
using AuthServer.Domain.Entities;
using AuthServer.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthServer.Persistence.Repositories.Products
{
    public sealed class ProductReadRepository(AuthServerDbContext _authServerDbContext) : IReadRepository<Product>
    {
        public DbSet<Product> Table => _authServerDbContext.Set<Product>();

        public IQueryable<Product> GetAll(bool tracking = false) => Table.AsQueryable();

        public async Task<Product> GetByIdAsync(string id, bool tracking = false) => await Table.FindAsync(id);

        public async Task<Product> GetSingleAsync(Expression<Func<Product, bool>> method, bool tracking = false) => await Table.FirstOrDefaultAsync(method);

        public IQueryable<Product> GetWhere(Expression<Func<Product, bool>> method, bool tracking = false) => Table.Where(method);
    }

}
