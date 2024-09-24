using System.Linq.Expressions;

namespace HiTech.Shared.Repositories;

/// <summary>
/// Generic interface for repositories.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IGenericRepository<TEntity, TKey>
    where TEntity : class, new()
    where TKey : IEquatable<TKey>
{
    //TEntity Create(TEntity t);

    //TEntity? GetByID(TKey id);

    //IEnumerable<TEntity> GetAll();

    //bool Update(TEntity t);

    //bool Delete(TEntity t);

    ValueTask<TEntity> CreateAsync(TEntity entity);

    ValueTask<TEntity?> GetByIDAsync(TKey id);

    ValueTask<IEnumerable<TEntity>> GetAllAsync();

    void Update(TEntity entity);

    void Delete(TEntity entity);

    ValueTask CreateRangeAsync(IEnumerable<TEntity> entities);

    void DeleteRange(IEnumerable<TEntity> entities);

    IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> expression);

    ValueTask<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> expression);
}
