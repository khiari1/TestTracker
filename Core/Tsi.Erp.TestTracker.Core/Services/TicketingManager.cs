using Tsi.Erp.TestTracker.Abstractions;

namespace Tsi.Erp.TestTracker.Core.Services
{

    public class TicketingManager
    {
        private readonly ITicketingSystem _ticketingSystem;

        public TicketingManager(ITicketingSystem ticketingSystem)
        {
            _ticketingSystem = ticketingSystem;
        }
    }
}

