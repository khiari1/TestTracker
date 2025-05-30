using System.Linq.Expressions;

namespace Tsi.Erp.TestTracker.Domain.Repositories;
public interface IGenericRepository<TEntity> where TEntity : IEntity
{
    Task<IEnumerable<TEntity>> GetAsync();
    Task<IEnumerable<TEntity>> GetAsync(Query filter);
    Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
    TEntity? Find(int id);
    Task<TEntity?> Find(Expression<Func<TEntity, bool>> predicate);
    void Create(TEntity entity);
    void Update(TEntity entity);

    void Delete(int id);
    void Delete(TEntity entity);
    void DeleteRange(IEnumerable<TEntity> entities);
    Task DeleteRangeAsync(int[] id);
    void Save();
    Task SaveAsync();
}
