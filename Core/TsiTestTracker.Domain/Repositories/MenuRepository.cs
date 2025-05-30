using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Erp.TestTracker.Infrastructure.Context;

namespace Tsi.Erp.TestTracker.Infrastructure.Repositories
{
    public class MenuRepository:GenericRepository<Menu> , IMenuRepository
    {
        public MenuRepository(IDbContextFactory<TestTrackerContext> dbContextFactory)
           : base(dbContextFactory)
        {
        }
        public override async Task<IEnumerable<Menu>> GetAllAsync()
        {
            return await  table.Include(m =>m.Module)
                .ToListAsync(); 
        }

    }
}
