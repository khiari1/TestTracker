namespace Tsi.Erp.TestTracker.Abstractions.Models.Job
{
    public class ScheduledJobResponse
    {
        //public Job Job { get; set; }
        public string Id { get; set; }
        public Exception LoadException { get; set; }

        public InvocationData InvocationData { get; set; }

        public DateTime EnqueueAt { get; set; }

        public DateTime? ScheduledAt { get; set; }

        public bool InScheduledState { get; set; }

        public IDictionary<string, string> StateData { get; set; }

    }
}
