using HiTech.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

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

    /// <inheritdoc/>
    public virtual TEntity Create(TEntity t)
    {
        var result = DbSet.Add(t);
        _context.SaveChanges();
        return result.Entity;
    }

    /// <inheritdoc/>
    public virtual TEntity? GetByID(TKey id) => DbSet.Find(id);

    /// <inheritdoc/>
    public virtual IEnumerable<TEntity> GetAll() => DbSet;

    /// <inheritdoc/>
    public virtual bool Update(TEntity t)
    {
        DbSet.Update(t);
        return _context.SaveChanges() > 0;
    }

    /// <inheritdoc/>
    public virtual bool Delete(TEntity t)
    {
        DbSet.Remove(t);
        return _context.SaveChanges() > 0;
    }

    /// <inheritdoc/>
    public virtual async ValueTask<TEntity> CreateAsync(TEntity t)
    {
        var result = await DbSet.AddAsync(t);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    /// <inheritdoc/>
    public virtual async ValueTask<TEntity?> GetByIDAsync(TKey id) => await DbSet.FindAsync(id);

    /// <inheritdoc/>
    public virtual IAsyncEnumerable<TEntity> GetAllAsync() => DbSet.AsAsyncEnumerable();

    /// <inheritdoc/>
    public virtual async ValueTask<bool> UpdateAsync(TEntity t)
    {
        DbSet.Update(t);
        return await _context.SaveChangesAsync(CancellationToken.None) > 0;
    }

    /// <inheritdoc/>
    public virtual async ValueTask<bool> DeleteAsync(TEntity t)
    {
        DbSet.Remove(t);
        return await _context.SaveChangesAsync(CancellationToken.None) > 0;
    }
}
