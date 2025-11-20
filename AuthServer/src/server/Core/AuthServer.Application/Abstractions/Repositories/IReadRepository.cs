using System.Linq.Expressions;

namespace AuthServer.Application.Abstractions.Repositories
{
    public interface IReadRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll(bool tracking = false);
        IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> method, bool tracking = false);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> method, bool tracking = false);
        Task<TEntity> GetByIdAsync(string id, bool tracking = false);
    }
}
