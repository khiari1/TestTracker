using System.Linq.Extensions;

namespace Tsi.Erp.TestTracker.Api.Controllers.Dto.Request
{
    public class QueryStoreModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Area { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public bool IsShared { get; set; }
        public Query Query { get; set; }
    }
}