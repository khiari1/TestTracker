using Tsi.Erp.TestTracker.Domain.Views;

namespace Tsi.Erp.TestTracker.Infrastructure.Repositories
{
    public interface IModuleSettingsRepository : IGenericRepository<Module>
    {
        public Task<IEnumerable<SettingsView>> GetModuleSettings();
        public Task<IEnumerable<SettingsView>> GetModuleSettings(string searchPattern);
    }
}