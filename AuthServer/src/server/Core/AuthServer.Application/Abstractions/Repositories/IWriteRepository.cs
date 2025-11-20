using System.Linq.Expressions;

namespace AuthServer.Application.Abstractions.Repositories
{
    public interface IWriteRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task<int> ExecuteDeleteAsync(Expression<Func<TEntity, bool>> predicate);
        //Task<int> ExecuteUpdateAsync(Expression<Func<TEntity, bool>> predicate,Action<TEntity> updateAction);
        Task<int> SaveChangesAsync();
    }
}
