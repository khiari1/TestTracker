using Tsi.Erp.TestTracker.Domain.Abstraction;
using Tsi.Erp.TestTracker.Infrastructure.Context;

namespace Tsi.Erp.TestTracker.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
{
    protected TestTrackerContext _context;
    protected readonly DbSet<T> table;
    public GenericRepository(IDbContextFactory<TestTrackerContext> dbContextFactory)
    {
        this._context =  dbContextFactory.CreateDbContext();
        table = _context.Set<T>();
    }
    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await table.ToListAsync();
    }
    public virtual T? GetById(int id) => 
        table.Find(id);

    public virtual T? Get(Func<T, bool> predicate) =>
        table.FirstOrDefault(predicate);
    public virtual void Create(T entity)
    {
        table.Add(entity);
    }
    public virtual void Update(T entity)
    {
        if(_context.Entry(entity).State == EntityState.Detached)
            table.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }
    public virtual void Delete(int id)
    {
        var existing = table.Find(id);
        if (existing == null)
        {
            throw new Exception("Can not find entity from data base") ;
        }

        table.Remove(existing);
    }

    public virtual void Save()
    {
        _context.SaveChanges();
    }

    public virtual async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
