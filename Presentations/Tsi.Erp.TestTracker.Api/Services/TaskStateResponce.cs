namespace Tsi.Erp.TestTracker.Api.Services
{
    public class TaskStateResponce
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskState State { get; set; }
    }

    public enum TaskState
    {
        Completed,
        Pending,
        Inprogress,
        Canceled,
        CanceledByUser,
        Failed
    }
}
