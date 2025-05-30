namespace Tsi.Erp.TestTracker.Domain.Stores;
public class MonitoringDetail : EntityBase
{
    public DateTime Date { get; set; }
    public Status Status { get; set; }
    public string? Message { get; set; }

    public string? ExceptionType { get; set; }
    public string? ExceptionMessage { get; set; }
    public string? StackTrace { get; set; }
    public string? Ticket { get; set; }
    public string? BuildVersion { get; set; }
    public TimeSpan Duration { get; set; }
    public virtual Monitoring Monitoring { get; set; }
    public int MonitoringId { get; set; }
}
