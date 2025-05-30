namespace Tsi.Erp.TestTracker.Api.Controllers.Dto.Request
{
    public class CommentModel
    {
        public int Id { get; set; }
        public string KeyGroup { get; set; } = string.Empty;
        public int ObjectId { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
