using System.Linq.Expressions;
using CinemaCatalog.Application.IRepository;
using CinemaCatalog.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CinemaCatalog.Infrastructure.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly CinemaCatalogContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(CinemaCatalogContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _dbSet;

        if (includeProperties != null)
        {
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
        }

        return await query.ToListAsync();
    }

    public IQueryable<TEntity> Query()
    {
        return _dbSet;
    }
    
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<TEntity> FindByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<TEntity> RemoveByIdAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    
    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        
        await _context.SaveChangesAsync();
        return entity;
    }
}