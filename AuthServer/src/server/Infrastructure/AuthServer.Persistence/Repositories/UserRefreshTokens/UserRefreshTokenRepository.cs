using AuthServer.Application.Abstractions.Repositories;
using AuthServer.Domain.Entities;
using AuthServer.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthServer.Persistence.Repositories.UserRefreshTokens
{
    public sealed class UserRefreshTokenRepository(AuthServerDbContext _authServerDbContext) : IReadRepository<UserRefreshToken>, IWriteRepository<UserRefreshToken>
    {
        public DbSet<UserRefreshToken> Table => _authServerDbContext.Set<UserRefreshToken>();

        public async Task AddAsync(UserRefreshToken entity) => await Table.AddAsync(entity);

        public async Task AddRangeAsync(IEnumerable<UserRefreshToken> entities) => await Table.AddRangeAsync(entities);

        public async Task<int> ExecuteDeleteAsync(Expression<Func<UserRefreshToken, bool>> predicate) => await Table.Where(predicate).ExecuteDeleteAsync();

        public IQueryable<UserRefreshToken> GetAll(bool tracking = false) => Table.AsQueryable();

        public async Task<UserRefreshToken> GetByIdAsync(string id, bool tracking = false) => await Table.FindAsync(id);

        public async Task<UserRefreshToken> GetSingleAsync(Expression<Func<UserRefreshToken, bool>> method, bool tracking = false) => await Table.SingleOrDefaultAsync(method);

        public IQueryable<UserRefreshToken> GetWhere(Expression<Func<UserRefreshToken, bool>> method, bool tracking = false) => Table.Where(method);

        public async Task<int> SaveChangesAsync() => await _authServerDbContext.SaveChangesAsync();
    }

}
