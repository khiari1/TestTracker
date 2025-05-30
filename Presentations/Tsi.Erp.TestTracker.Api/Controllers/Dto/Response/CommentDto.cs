using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tsi.Erp.TestTracker.Domain.Stores;

namespace Tsi.Erp.TestTracker.Api.Controllers.Dto.Response
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string KeyGroup { get; set; } = string.Empty;
        public int ObjectId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
