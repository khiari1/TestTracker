

namespace Tsi.Erp.TestTracker.Domain.Stores
{
    public class QueryStore : EntityBase
    {
        public string Name { get; set; }
        public string Area { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public bool IsShared {  get; set; } 
        public string SerializedQuery { get; set; }
    }
}
