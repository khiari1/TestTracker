namespace Tsi.Erp.TestTracker.Api.Controllers.Dto.Response;
public class ModuleDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

}
public class ModuleSettingsDto
{
    public string Name { get; set; } = string.Empty;
    public int MenuId { get; set; }
    public string MenuName { get; set; } = string.Empty;
    public int SubMenuId { get; set; }
    public string SubMenuName { get; set; } = string.Empty;
}
