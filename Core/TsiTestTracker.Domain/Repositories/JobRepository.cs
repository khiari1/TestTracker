using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Erp.TestTracker.Infrastructure.Context;

namespace Tsi.Erp.TestTracker.Infrastructure.Repositories
{
    public class JobRepository : GenericRepository<Job> 
    {
        public JobRepository(IDbContextFactory<TestTrackerContext> dbContextFactory)
            : base(dbContextFactory)
        {
        }
    
    }
}
