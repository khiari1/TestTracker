using Tsi.Erp.TestTracker.Domain.Stores;

namespace Tsi.Erp.TestTracker.Domain.Views
{
    public class MonitoringDetailView
    {
        public int Id { get; set; }
        public string ModuleName { get; set; }
        public Status? State { get; set; }
        public string? ErrorMesage { get; set; }
        public DateTime? Date { get; set; }
        public string? BuildVersion { get; set; }
        public string? Preconditions { get; set; }
        public string? UseCase { get; set; }
        public string? AwaitedResult { get; set; }
        public int ModuleId { get; set; }
        public string NameMethodeTest { get; set; }
        public DateTime FailingSince { get; set; }
        public string? Ticket { get; set; }
        public TimeSpan? Duration { get; set; }
        public string? StackTrace { get; set; }
        public string? TesterName { get; set; }
        public string? TesterId { get; set; }
        public string? ResponcibleName { get; set; }
        public string? ResponcibleId { get; set; }
        public string? ExceptionType { get; set; }
        public string? ExceptionMessage { get; set; }
        public MonitoringTestType MonitoringTestType { get; set; }
    }
}