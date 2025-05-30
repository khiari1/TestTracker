using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsi.Erp.TestTracker.Domain.Stores;

public class Job : EntityBase
{

    public string Name { get; set; } = string.Empty;
    public int TaskId { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime Startdate { get; set; }
    public DateTime Finishdate { get; set; }
    public JobEnum JobState { get; set; }
}
public enum JobEnum
{
    Completed,
    Inprogress,
    Canceled,
    Failed

}

