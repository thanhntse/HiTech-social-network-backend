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

    protected GenericRepository(T context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    protected DbSet<TEntity> DbSet => _context.Set<TEntity>();

    ///// <inheritdoc/>
    //public virtual TEntity Create(TEntity t)
    //{
    //    var result = DbSet.Add(t);
    //    _context.SaveChanges();
    //    return result.Entity;
    //}

    ///// <inheritdoc/>
    //public virtual TEntity? GetByID(TKey id) => DbSet.Find(id);

    ///// <inheritdoc/>
    //public virtual IEnumerable<TEntity> GetAll() => DbSet;

    ///// <inheritdoc/>
    //public virtual bool Update(TEntity t)
    //{
    //    DbSet.Update(t);
    //    return _context.SaveChanges() > 0;
    //}

    ///// <inheritdoc/>
    //public virtual bool Delete(TEntity t)
    //{
    //    DbSet.Remove(t);
    //    return _context.SaveChanges() > 0;
    //}

    /// <inheritdoc/>
    public virtual async ValueTask<TEntity> CreateAsync(TEntity entity)
    {
        var result = await DbSet.AddAsync(entity);
        return result.Entity;
    }

    /// <inheritdoc/>
    public virtual async ValueTask<TEntity?> GetByIDAsync(TKey id)
        => await DbSet.FindAsync(id);

    /// <inheritdoc/>
    public virtual async ValueTask<IEnumerable<TEntity>> GetAllAsync()
        => await DbSet.ToListAsync();

    /// <inheritdoc/>
    public virtual void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    /// <inheritdoc/>
    public virtual void Delete(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    /// <inheritdoc/>
    public virtual async ValueTask CreateRangeAsync(IEnumerable<TEntity> entities)
    {
        await DbSet.AddRangeAsync(entities);
    }

    /// <inheritdoc/>
    public virtual void DeleteRange(IEnumerable<TEntity> entities)
    {
        DbSet.RemoveRange(entities);
    }

    /// <inheritdoc/>
    public virtual IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> expression)
        => DbSet.Where(expression);

    /// <inheritdoc/>
    public virtual async ValueTask<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> expression)
        => await DbSet.Where(expression).ToListAsync();
}
