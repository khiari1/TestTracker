using Hangfire.Common;
using Hangfire.Storage;

namespace Tsi.Erp.TestTracker.Api.Controllers.Dto.Response
{
    public class EnqueuedJobResponse
    {
        //public Job Job { get; set; }
        public string Id { get; set; }
        public JobLoadException LoadException { get; set; }

        public InvocationData InvocationData { get; set; }

        public string State { get; set; }

        public DateTime? EnqueuedAt { get; set; }

        public bool InEnqueuedState { get; set; }

        public IDictionary<string, string> StateData { get; set; }
    }
}
