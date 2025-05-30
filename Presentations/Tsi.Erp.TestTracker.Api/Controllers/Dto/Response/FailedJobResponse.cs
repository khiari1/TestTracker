using Hangfire.Common;
using Hangfire.Storage;

namespace Tsi.Erp.TestTracker.Api.Controllers.Dto.Response
{
    public class FailedJobResponse
    {
        //public Job Job { get; set; }
        public string Id { get; set; }
        public JobLoadException LoadException { get; set; }

        public InvocationData InvocationData { get; set; }

        public string Reason { get; set; }

        public DateTime? FailedAt { get; set; }

        public string ExceptionType { get; set; }

        public string ExceptionMessage { get; set; }

        public string ExceptionDetails { get; set; }

        public bool InFailedState { get; set; }

        public IDictionary<string, string> StateData { get; set; }
    }
}
