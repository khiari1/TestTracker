using Tsi.Erp.TestTracker.Domain.Repositories;
using Tsi.Erp.TestTracker.Domain.Stores;
using Tsi.Erp.TestTracker.EntityFrameworkCore.Context;

namespace Tsi.Erp.TestTracker.EntityFrameworkCore.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        protected TestTrackerContext _context;

        public ResourceRepository(IDbContextFactory<TestTrackerContext> dbContextFactory)
        {
            _context = dbContextFactory.CreateDbContext();
        }     
        public async Task<List<KeyValuePair<int, string>>> GetModules(string? searchPattern)
        {
            var query = _context.Modules as IQueryable<Module>;
            if (searchPattern is not null)
            {
                query = query.Where(f => f.Name.Contains(searchPattern));
            }

            return await query
                .Select(f => new KeyValuePair<int, string>(f.Id, f.Name))
                .ToListAsync();
        }
        public async Task<List<KeyValuePair<string, string>>> GetUsersAsync(string? serachPattern)
        {
            return await _context.TsiUsers.Select(u => new KeyValuePair<string, string>(u.Id, u.DisplayName))
                .ToListAsync();
        }
    }
}
