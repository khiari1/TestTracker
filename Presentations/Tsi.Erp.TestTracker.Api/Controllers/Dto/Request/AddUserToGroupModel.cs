namespace Tsi.Erp.TestTracker.Api.Controllers.Dto.Request
{
    public class UserToGroupModel
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
    }

    public class UserToGroupRquest
    {
        public string GroupId { get; set; }
        public string UserId { get; set; }
    }
}
