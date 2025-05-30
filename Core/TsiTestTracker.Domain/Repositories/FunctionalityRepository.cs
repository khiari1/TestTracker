using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Erp.TestTracker.Infrastructure.Context;

namespace Tsi.Erp.TestTracker.Infrastructure.Repositories
{
    public class FunctionalityRepository : GenericRepository<Functionality>
    {
        public FunctionalityRepository(IDbContextFactory<TestTrackerContext> dbContextFactory) : base(dbContextFactory)
        {

        }
        public override async Task<IEnumerable<Functionality>> GetAllAsync() =>
             await table.Include(s => s.SubMenu)
                .ThenInclude(m => m.Menu)
            .ThenInclude(m => m.Module)
                .ToListAsync();

        public async Task<IEnumerable<Functionality>> GetAllAsync(CancellationToken cancellationToken) =>
             await table.Include(s => s.SubMenu)
                .ThenInclude(m => m.Menu)
            .ThenInclude(m => m.Module)
                .ToListAsync(cancellationToken);

        public override Functionality? GetById(int id) =>
           table.Include(s => s.SubMenu)
                .ThenInclude(m => m.Menu)
            .ThenInclude(m => m.Module)
                .FirstOrDefault(s => s.Id == id);
    }
}
