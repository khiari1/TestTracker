using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Erp.TestTracker.Infrastructure.Context;

namespace Tsi.Erp.TestTracker.Infrastructure.Repositories
{
    public class AssemblyRepository : GenericRepository<AssemblyFile>, IAssemblyRepository
    {
        public AssemblyRepository(IDbContextFactory<TestTrackerContext> dbContextFactory)
            : base(dbContextFactory)
        {
           
        }
    }
}
