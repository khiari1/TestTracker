namespace Tsi.Erp.TestTracker.Abstractions.Models.Job
{
    public class EnqueuedJobResponse
    {
        //public Job Job { get; set; }
        public string Id { get; set; }
        public Exception LoadException { get; set; }

        public InvocationData InvocationData { get; set; }

        public string State { get; set; }

        public DateTime? EnqueuedAt { get; set; }

        public bool InEnqueuedState { get; set; }

        public IDictionary<string, string> StateData { get; set; }
    }
}
