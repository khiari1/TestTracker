using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsi.Erp.TestTracker.Domain.Stores
{
    public class Attachement : EntityBase
    {
        public string Folder { get; set; } = string.Empty;
        public int? ObjectId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string Extension { get; set; } = string.Empty;
        public int FileSize { get; set; }
        public byte[] Data { get; set; } = new byte[0];
        public DateTime Date { get; set; }
        public ApplicationUser? User { get; set; }
        public string? UserId { get; set; }
    }
}
