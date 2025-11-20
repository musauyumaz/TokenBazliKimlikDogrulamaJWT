using Microsoft.EntityFrameworkCore;

namespace AuthServer.Application.Abstractions.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> Table { get; }
    }
}
