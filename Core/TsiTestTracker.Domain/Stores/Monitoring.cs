namespace Tsi.Erp.TestTracker.Domain.Stores;
public class Monitoring : EntityBase

{
    public string NameMethodTest { get; set; }
    public string? UseCase { get; set; }
    public string? Preconditions { get; set; }
    public string? AwaitedResult { get; set; }
    public MonitoringTestType MonitoringTestType { get; set; }
    public DateTime FailingSince { get; set; }
    public virtual ApplicationUser? Tester { get; set; }
    public string? TesterId { get; set; }
    public virtual ApplicationUser? Responsible { get; set; }
    public string? ResponsibleId { get; set; }
    public virtual ICollection<MonitoringDetail> MonitoringDetails { get; set; }
}

public enum MonitoringTestType
{
    FunctionalTest,
    UnitTest
}
