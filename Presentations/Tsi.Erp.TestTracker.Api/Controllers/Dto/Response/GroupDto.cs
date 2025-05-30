namespace Tsi.Erp.TestTracker.Api.Controllers.Dto.Response
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;


        public string Description { get; set; } = string.Empty;
        public virtual IEnumerable<UserDto>? UserGroups { get; set; }

        public virtual IEnumerable<PermissionResponse>? Permissions { get; set; }
    }

    public class GroupResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

}
