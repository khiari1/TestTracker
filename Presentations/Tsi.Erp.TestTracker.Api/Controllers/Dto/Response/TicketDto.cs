namespace Tsi.Erp.TestTracker.Api.Controllers.Dto.Response
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string? TicketTypeName { get; set; } = string.Empty;
        public string? TicketColor { get; set; } = string.Empty;
        public int TicketTypeId { get; set; }
        public string? StateColor { get; set; } = string.Empty;
        public string? StateName { get; set; } = string.Empty;
        public int StateId { get; set; }
        public string? AssignedToName { get; set; } = string.Empty;
        public int AssignedToId { get; set; }
        public string? ModuleName { get; set; } = string.Empty;
        public string? MenuName { get; set; } = string.Empty;
        public string? SubMenuName { get; set; } = string.Empty;
        public int SubMenuId { get; set; }
        public int MenuId { get; set; }
        public int ModuleId { get; set; }
    }
}
