using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Erp.TestTracker.Domain.Models;
using Tsi.Erp.TestTracker.Infrastructure.Context;

namespace Tsi.Erp.TestTracker.Infrastructure.Repositories
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
            var value= await Settings.Where(v=>v.Equals(Key)).Select(v=>v.Value).FirstOrDefaultAsync();
            return value;
        }
    }
    public interface ISettingRepository
    {
        Task<string?> GetValueByKeyAsync(String Key);
        Task<Settings?> GetByKeyAsync(String Key);
        Task<bool> ExistAsync(string Key);
        Task CreateAsync(Settings setting);
        Task UpdateAsync(Settings setting);
    }
}
