using Tsi.Erp.TestTracker.Domain.Repositories;
using Tsi.Erp.TestTracker.Domain.Stores;
using Tsi.Erp.TestTracker.EntityFrameworkCore.Context;

namespace Tsi.Erp.TestTracker.EntityFrameworkCore.Repositories
{
    public class AssemblyRepository : GenericRepository<ProjectFile>, IAssemblyRepository
    {
        public AssemblyRepository(IDbContextFactory<TestTrackerContext> dbContextFactory)
            : base(dbContextFactory)
        {

        }
    }
}
