using Tsi.Erp.TestTracker.Domain.Stores;
using Tsi.Erp.TestTracker.EntityFrameworkCore.Context;
using Tsi.Erp.TestTracker.EntityFrameworkCore.Repositories;

namespace Tsi.Erp.TestTracker.EntityFrameworkCore.Repositories
{
    public class SettingsRepository : ISettingRepository
    {

        private readonly DbContext _dbContext;

        protected DbSet<Settings> Settings { get => _dbContext.Set<Settings>(); }

        public SettingsRepository(IDbContextFactory<TestTrackerContext> dbContextFactory)

        {
            _dbContext = dbContextFactory.CreateDbContext();
        }

        public async Task CreateAsync(Settings setting)
        {
            if (!ExistAsync(setting.Key).Result)
            {
                await Settings.AddAsync(setting);
                _dbContext.SaveChanges();
            }
        }

        public async Task<bool> ExistAsync(string key)
        {

            return await Settings.AnyAsync(v => v.Key == key);
        }



        public async Task<Settings?> GetByKeyAsync(string Key)
        {

            var setting = await Settings.Where(s => s.Key == Key).FirstOrDefaultAsync();
            return setting;
        }

        public async Task UpdateAsync(Settings settings)
        {
            if (await ExistAsync(settings.Key))
            {
                Settings.Update(settings);
                _dbContext.SaveChanges();

            }


        }

        public async Task<string?> GetValueByKeyAsync(string Key)
        {
            var value = await Settings.Where(v => v.Equals(Key)).Select(v => v.Value).FirstOrDefaultAsync();
            return value;
        }
    }
    public interface ISettingRepository
    {
        Task<string?> GetValueByKeyAsync(string Key);
        Task<Settings?> GetByKeyAsync(string Key);
        Task<bool> ExistAsync(string Key);
        Task CreateAsync(Settings setting);
        Task UpdateAsync(Settings setting);
    }
}
