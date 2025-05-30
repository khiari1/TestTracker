using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Erp.TestTracker.Infrastructure.Context;

namespace Tsi.Erp.TestTracker.Infrastructure.Repositories
{
    public class SubMenuRepository : GenericRepository<SubMenu>
    {
        
        public SubMenuRepository(IDbContextFactory<TestTrackerContext> dbContextFactory) 
            : base(dbContextFactory)
        {
        }
        public override async Task<IEnumerable<SubMenu>> GetAllAsync()=>
             await table.Include(s=>s.Menu)
                .ThenInclude(m=>m.Module)
                .ToListAsync();
        
        public override SubMenu? GetById(int id) => 
            table.Include(s=>s.Menu)
                .ThenInclude(m=>m.Module)
                .FirstOrDefault(s=>s.Id == id);
        
    }
}
