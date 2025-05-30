using Tsi.Erp.TestTracker.Domain.Stores;

namespace Tsi.Erp.TestTracker.Api.Controllers.Dto.Request;
public class MonitoringModel
{

    public int Id { get; set; }
    public string NameMethodTest { get; set; }
    public string? UseCase { get; set; }
    public string? Preconditions { get; set; }
    public string? Comment { get; set; }
    public string? AwaitedResult { get; set; }
    public string? TesterId { get; set; }
    public string? ResponsibleId { get; set; }
    public MonitoringTestType MonitoringTestType { get; set; }
}
