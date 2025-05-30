using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tsi.Erp.TestTracker.Domain.Stores
{
    public class ProjectFile : EntityBase
    {
       
        public string FileName { get; set; }
        public string ProjectName { get; set; }
        public byte[] Data { get; set; }
        public int Size { get; set; }


    }
}
