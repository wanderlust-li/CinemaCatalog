using System.Linq.Expressions;

namespace CinemaCatalog.Application.IRepository;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includeProperties);

    IQueryable<TEntity> Query();
    
    Task<TEntity> AddAsync(TEntity entity);
    
    Task SaveChangesAsync();
    
    Task<TEntity> FindByIdAsync(int id);

    Task<TEntity> RemoveByIdAsync(int id);

    Task<TEntity> UpdateAsync(TEntity entity);
}