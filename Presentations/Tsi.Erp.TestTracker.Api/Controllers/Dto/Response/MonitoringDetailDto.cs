using Tsi.Erp.TestTracker.Domain.Stores;

namespace Tsi.Erp.TestTracker.Api.Controllers.Dto.Response;

public class MonitoringDetailDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public Status Status { get; set; }
    public string? Message { get; set; }
    public string? StackTrace { get; set; }
    public string? Ticket { get; set; }
    public string? BuildVersion { get; set; }
    public string? ExceptionType { get; set; }
    public TimeSpan Duration { get; set; }
}
