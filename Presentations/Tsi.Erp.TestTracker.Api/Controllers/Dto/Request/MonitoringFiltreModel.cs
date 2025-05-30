using Tsi.Erp.TestTracker.Domain.Stores;

namespace Tsi.Erp.TestTracker.Api.Controllers.Dto.Request
{
    public class MonitoringFiltreModel
    {
        public string SearchPattern { get; set; }
        public DateTime? Date { get; set; }
        public Status[]? Status { get; set; }  
        public int[]? ModuleIds { get; set; }

    }
}
