using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Erp.TestTracker.Infrastructure.Context;

namespace Tsi.Erp.TestTracker.Infrastructure.Repositories
{
    public class TicketRepository : GenericRepository<Ticket>
    {
        public TicketRepository(IDbContextFactory<TestTrackerContext> dbContextFactory) 
            : base(dbContextFactory)
        {
        }

        public override async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            var ticket = table.Include(p => p.AssignedTo)
                .Include(p => p.TicketType)
                .Include(p => p.State)
                .Include(p => p.Functionality);
            return await ticket.ToListAsync();
        }

        public override Ticket? GetById(int id)
        {
            var ticket = table.Include(p => p.AssignedTo)
                .Include(p => p.TicketType)
                .Include(p => p.State)
                .Include(p => p.Functionality);
            return ticket.FirstOrDefault(t=>t.Id == id);
        }
    }
}
