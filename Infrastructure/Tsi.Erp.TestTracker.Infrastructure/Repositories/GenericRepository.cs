using System.Linq.Expressions;
using System.Linq.Extensions;
using Tsi.Erp.TestTracker.Domain.Repositories;
using Tsi.Erp.TestTracker.EntityFrameworkCore.Context;

namespace Tsi.Erp.TestTracker.EntityFrameworkCore.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : EntityBase
{
    protected TestTrackerContext _context;
    protected readonly DbSet<TEntity> table;
    public GenericRepository(IDbContextFactory<TestTrackerContext> dbContextFactory)
    {
        _context = dbContextFactory.CreateDbContext();
        table = _context.Set<TEntity>();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAsync()
    {
        return await table.ToListAsync();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAsync(Query filter)
    {
        return await table.Where(filter).ToListAsync();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await table.Where(predicate).ToListAsync();
    }

    public virtual TEntity? Find(int id) =>
        table.Find(id);

    public async virtual Task<TEntity?> Find(Expression<Func<TEntity, bool>> predicate) =>
        await table.FirstOrDefaultAsync(predicate);
    public virtual void Create(TEntity entity)
    {
        table.Add(entity);
    }
    public virtual void Update(TEntity entity)
    {
        table.Update(entity);

    }
    public virtual void Delete(int id)
    {
        var existing = Find(id);
        if (existing == null)
        {
            throw new Exception("Can not find entity from data base");
        }

        table.Remove(existing);
    }

    public virtual async Task DeleteRangeAsync(int[] ids)
    {
        var entities = await GetAsync(e => ids.Contains(e.Id));

        DeleteRange(entities);
    }
    public virtual void Delete(TEntity entity)
    {
        table.Remove(entity);
    }

    public virtual void DeleteRange(IEnumerable<TEntity> entities)
    {
        table.RemoveRange(entities);
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

