namespace Tsi.Erp.TestTracker.Abstractions.Models.Job
{
    public class ProcessingJobResponse
    {
        public IDictionary<string, string> StateData { get; set; }
        public DateTime? StartedAt { get; set; }
        public InvocationData InvocationData { get; set; }
        public bool InProcessingState { get; set; }
        public string Id { get; set; }
        public Exception LoadException { get; set; }
    }
}
