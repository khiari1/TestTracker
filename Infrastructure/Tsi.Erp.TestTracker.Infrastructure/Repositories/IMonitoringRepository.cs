namespace Tsi.Erp.TestTracker.Infrastructure.Repositories
{
    public interface IMonitoringRepository : IGenericRepository<Monitoring>
    {
        Task<IEnumerable<ModuleSummary>> GetSummaryByModuleAsync(string? searchPattern, DateTime? date, int[]? moduleIds);        
        Task<IEnumerable<Monitoring>> GetMonitoringWithDetailsAsync(string? searchPattern = null);
        Task<MonitoringDetailView?> GetMonitoringDetailAsync(int monitoringDetailId);
        Task<IEnumerable<MonitoringDetailView>> GetMonitoringDetailsAsync(string? searchPattern, DateTime? date, State? state, int[]? moduleIds);
        Task<IEnumerable<MonitoringDetailView>> GetDetailsByModuleIdAsync(int id,string? searchPattern, DateTime? date, State? state);
        Task<IEnumerable<MonitoringDetailView>> GetDetailsByMenuIdAsync(int id);
        Task<IEnumerable<MonitoringDetailView>> GetDetailsBySubMenuIdAsync(int id);
        Task<IEnumerable<MonitoringDetailView>> GetDetailsByFunctionalityIdAsync(int id);
        Task<int> MonitoringDetailCountAsync(DateTime? date, State? state);
        Task<IEnumerable<MonitoringDetailChart>> GetAllMonitoringDetailAsync();
        Task<int> GetCountMonitoringByStateAsync(State? state);   
         Task<IEnumerable<MonitoringDetailChart>> GetMonitoringByStateAsync(State state);
        Task DeleteAllMonitoringDetailsAsync();

    }
}