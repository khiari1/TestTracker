using Tsi.Erp.TestTracker.Domain.Repositories;
using Tsi.Erp.TestTracker.Domain.Stores;
using Tsi.Erp.TestTracker.Domain.Views;
using Tsi.Erp.TestTracker.EntityFrameworkCore.Context;
using Tsi.Erp.TestTracker.EntityFrameworkCore.Repositories;

namespace Tsi.Erp.TestTracker.EntityFrameworkCore.Repositories
{
    public class ModuleRepository : GenericRepository<Module>, IModuleRepository
    {
        public ModuleRepository(IDbContextFactory<TestTrackerContext> dbContextFactory) : base(dbContextFactory)
        {
        }


    }
}
