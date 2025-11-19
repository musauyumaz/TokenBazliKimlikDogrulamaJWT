using AuthServer.Application.Abstractions.Repositories;
using AuthServer.Domain.Entities;
using AuthServer.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthServer.Persistence.Repositories.Products;

public sealed class ProductWriteRepository(AuthServerDbContext _authServerDbContext) : IWriteRepository<Product>
{
    public DbSet<Product> Table => _authServerDbContext.Set<Product>();
    public async Task AddAsync(Product entity) => await Table.AddAsync(entity);
    public async Task AddRangeAsync(IEnumerable<Product> entities) => await Table.AddRangeAsync(entities);
    public async Task<int> ExecuteDeleteAsync(Expression<Func<Product, bool>> predicate) => await Table.Where(predicate).ExecuteDeleteAsync();
    public async Task<int> SaveChangesAsync() => await _authServerDbContext.SaveChangesAsync();
}
