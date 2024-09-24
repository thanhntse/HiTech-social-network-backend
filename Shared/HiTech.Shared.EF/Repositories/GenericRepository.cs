using HiTech.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HiTech.Shared.EF.Repositories;

/// <summary>
/// (Sample) An abstract EF-implementation of IGenericRepository.
/// </summary>
public abstract class GenericRepository<T, TEntity, TKey> : IGenericRepository<TEntity, TKey>
    where T : DbContext
    where TEntity : class, new()
    where TKey : IEquatable<TKey>
{
    protected readonly T _context;
    protected readonly DbSet<TEntity> _dbSet;

    protected GenericRepository(T context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<TEntity>();
    }

    /// <inheritdoc/>
    public virtual async ValueTask<TEntity> CreateAsync(TEntity entity)
    {
        var result = await _dbSet.AddAsync(entity);
        return result.Entity;
    }

    /// <inheritdoc/>
    public virtual async ValueTask<TEntity?> GetByIDAsync(TKey id)
        => await _dbSet.FindAsync(id);

    /// <inheritdoc/>
    public virtual async ValueTask<IEnumerable<TEntity>> GetAllAsync()
        => await _dbSet.ToListAsync();

    /// <inheritdoc/>
    public virtual void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    /// <inheritdoc/>
    public virtual void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    /// <inheritdoc/>
    public virtual async ValueTask CreateRangeAsync(IEnumerable<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    /// <inheritdoc/>
    public virtual void DeleteRange(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    /// <inheritdoc/>
    public virtual IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> expression)
        => _dbSet.Where(expression);

    /// <inheritdoc/>
    public virtual async ValueTask<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> expression)
        => await _dbSet.Where(expression).ToListAsync();
}
