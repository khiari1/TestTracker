
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Tsi.Erp.TestTracker.Domain.Repositories;
using Tsi.Erp.TestTracker.Domain.Stores;

namespace Tsi.Erp.TestTracker.Core.Services
{
    public partial class SettingsService
    {
        private readonly IGenericRepository<Settings> _settingsRepository;

        public SettingsService(IGenericRepository<Settings> settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }


        public async Task SaveAsync<TObject>([NotNull] TObject value)
         where TObject : class
        {
            ArgumentNullException.ThrowIfNull(value);
            //var key = nameof(TObject);
            var key = typeof(TObject).Name;
            var serializedValue = JsonSerializer.Serialize(value);
            var setting = await _settingsRepository.Find(s => s.Key.Equals(key));
            if (setting is not null)
            {
                setting.Value = serializedValue;
                _settingsRepository.Update(setting);
            }
            else
            {
                _settingsRepository.Create(new Settings()
                {
                    Key = key,
                    Value = serializedValue
                });
            }

            await _settingsRepository.SaveAsync();

        }
        public async Task<TObject> GetSetting<TObject>()
            where TObject : class, new()
        {

            var key = typeof(TObject).Name;
            var setting = await _settingsRepository.Find(s => s.Key.Equals(key));

          if (setting is null)
            {
                await SaveAsync(new TObject());
            }
            setting = await _settingsRepository.Find(s => s.Key.Equals(key));

#pragma warning disable CS8603 // Existence possible d'un retour de référence null.

            return JsonSerializer.Deserialize<TObject>(setting.Value);

#pragma warning restore CS8603 // Existence possible d'un retour de référence null.





        }

    }
}
