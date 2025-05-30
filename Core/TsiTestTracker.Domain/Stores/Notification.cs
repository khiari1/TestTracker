using Tsi.Erp.TestTracker.Domain.Stores;

namespace Tsi.Erp.TestTracker.Domain.Stores
{
    public class Notification : EntityBase
    {
        public Notification()
        {
            Value = "";
            Subject = "";
            Message = "";
            Severity = "";
            Area = "";
            
        }
        public string? UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string Severity { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Area { get; set; }
        public int ObjectId { get; set; }
        public string Value { get; set; }
    }

    public enum Severity
    {
        Warning,
        Information,
        Error,
        Success,
    }
}
