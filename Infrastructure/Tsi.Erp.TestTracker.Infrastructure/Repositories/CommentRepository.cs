
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Erp.TestTracker.Domain.Repositories;
using Tsi.Erp.TestTracker.Domain.Stores;
using Tsi.Erp.TestTracker.EntityFrameworkCore.Context;

namespace Tsi.Erp.TestTracker.EntityFrameworkCore.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(IDbContextFactory<TestTrackerContext> dbContextFactory) : base(dbContextFactory)
        {

        }
        public async override Task<IEnumerable<Comment>> GetAsync()
        {
            return await table.Include(c => c.User).ToListAsync();
        }
        public override void Create(Comment entity)
        {
            base.Create(entity);
        }

    }
}
