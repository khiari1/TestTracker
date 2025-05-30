namespace Tsi.Erp.TestTracker.Api.Controllers.Dto.Response;

public class TaskDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int TaskId { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime Startdate { get; set; }
    public DateTime Finishdate { get; set; }
    public string State { get; set; } = string.Empty;
}

