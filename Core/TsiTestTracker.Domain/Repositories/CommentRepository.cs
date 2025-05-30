
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Erp.TestTracker.Abstractions;
using Tsi.Erp.TestTracker.Infrastructure.Context;

namespace Tsi.Erp.TestTracker.Infrastructure.Repositories
{
    public class CommentRepository : GenericRepository<Comment>,ICommentRepository
    {
        private readonly IUserManager<User> _userManagement;
        public CommentRepository(IDbContextFactory<TestTrackerContext> dbContextFactory, IUserManager<User> userManagement) : base(dbContextFactory)
        {
            _userManagement = userManagement;
        }
        public async override Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await table.Include(c => c.User).ToListAsync();
        }
        public override void Create(Comment entity)
        {
            var currentUser = _userManagement.CurrentUserAsync().Result;
            if (currentUser is not null)
            {
                entity.UserId = currentUser.Id;
            }
            
            base.Create(entity);
        }

    }
}
