using Tsi.Erp.TestTracker.Domain.Repositories;
using Tsi.Erp.TestTracker.Domain.Stores;
using Tsi.Erp.TestTracker.EntityFrameworkCore.Context;

namespace Tsi.Erp.TestTracker.EntityFrameworkCore.Repositories
{
    class LabelRepository : GenericRepository<Label>, ILabelRepository
    {
        public LabelRepository(IDbContextFactory<TestTrackerContext> dbContextFactory)
            : base(dbContextFactory)
        {
        }
    }
}