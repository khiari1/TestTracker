using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsi.Erp.TestTracker.Domain.Stores
{
    public class Comment : EntityBase
    {

        public string KeyGroup { get; set; } = string.Empty;
        public int ObjectId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
