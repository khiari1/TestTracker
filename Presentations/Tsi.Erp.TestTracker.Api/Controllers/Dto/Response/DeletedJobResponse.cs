using Hangfire.Common;
using Hangfire.Storage;

namespace Tsi.Erp.TestTracker.Api.Controllers.Dto.Response
{
    public class DeletedJobResponse
    {
        //public Job Job { get; set; }

        public string Id { get; set; }
        public JobLoadException LoadException { get; set; }

        public InvocationData InvocationData { get; set; }

        public DateTime? DeletedAt { get; set; }

        public bool InDeletedState { get; set; }

        public IDictionary<string, string> StateData { get; set; }
    }
}
