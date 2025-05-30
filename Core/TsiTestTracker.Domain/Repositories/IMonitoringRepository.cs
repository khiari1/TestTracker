using System.Linq.Extensions;
using Tsi.Erp.TestTracker.Domain.Stores;
using Tsi.Erp.TestTracker.Domain.Views;

namespace Tsi.Erp.TestTracker.Domain.Repositories
{
    public interface IMonitoringRepository : IGenericRepository<Monitoring>
    {

        Task<IEnumerable<MonitoringDetailView>> GetMonitoringsAsync(Query filter);
        Task<IEnumerable<MonitoringDetailView>> GetMonitoringDetailAsync(Query filter);

        Task DeleteMonitoringDetailsAsync(int[] ids);
        Task DeleteAll();
    }
}