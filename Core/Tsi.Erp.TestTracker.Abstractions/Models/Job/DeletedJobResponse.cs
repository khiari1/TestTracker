namespace Tsi.Erp.TestTracker.Abstractions.Models.Job
{
    public class DeletedJobResponse
    {
        //public Job Job { get; set; }

        public string Id { get; set; }
        public Exception LoadException { get; set; }

        public InvocationData InvocationData { get; set; }

        public DateTime? DeletedAt { get; set; }

        public bool InDeletedState { get; set; }

        public IDictionary<string, string> StateData { get; set; }
    }
}
