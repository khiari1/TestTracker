using Tsi.Erp.TestTracker.Domain.Abstraction;

namespace Tsi.Erp.TestTracker.Infrastructure.Repositories;
public interface IGenericRepository<T> where T : IEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    T? GetById(int id);
    T? Get(Func<T,bool> predicate);
    void Create(T obj);
    void Update(T obj);
    void Delete(int id);    
    void Save();
    Task SaveAsync();
}
