

using Hangfire.Common;
using Hangfire.Storage;

namespace Tsi.Erp.TestTracker.Api.Controllers.Dto.Response
{
    public class ProcessingJobResponse
    {
        public IDictionary<string, string> StateData { get; internal set; }
        public DateTime? StartedAt { get; internal set; }
        public InvocationData InvocationData { get; internal set; }
        public bool InProcessingState { get; internal set; }
        public string Id { get; internal set; }
        public JobLoadException LoadException { get; internal set; }
    }
}
