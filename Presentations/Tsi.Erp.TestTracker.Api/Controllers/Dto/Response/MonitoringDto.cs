using Tsi.Erp.TestTracker.Domain.Stores;

namespace Tsi.Erp.TestTracker.Api.Controllers.Dto.Response;

public class MonitoringDto
{

    public int Id { get; set; }
    public string NameMethodTest { get; set; }
    public string UseCase { get; set; }
    public string Preconditions { get; set; }
    public string AwaitedResult { get; set; }
    public DateTime FailingSince { get; set; } = DateTime.Now;
    public string ModuleName { get; set; }
    public string MenuName { get; set; }
    public string SubMenuName { get; set; }
    public string TesterId { get; set; }
    public string TesterName { get; set; }
    public string ResponsibleId { get; set; }
    public string ResponsibleName { get; set; }
    public MonitoringTestType MonitoringTestType { get; set; }

    public int ModuleId { get; set; }
    public int MenuId { get; set; }
    public int SubMenuId { get; set; }
    public virtual IEnumerable<MonitoringDetailDto> MonitoringDetails { get; set; }

}
