namespace Tsi.Erp.TestTracker.Abstractions.Models.Job
{
    public class SucceededJobResponse
    {
        //public Job Job { get; set; }
        public string Id { get; set; }
        public Exception LoadException { get; set; }

        public InvocationData InvocationData { get; set; }

        public object Result { get; set; }

        public long? TotalDuration { get; set; }

        public DateTime? SucceededAt { get; set; }

        public bool InSucceededState { get; set; }

        public IDictionary<string, string> StateData { get; set; }
    }
}
