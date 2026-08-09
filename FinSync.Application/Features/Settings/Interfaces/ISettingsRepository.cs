using FinSync.Domain.Entities;

namespace FinSync.Application.Features.Settings.Interfaces
{
    public interface ISettingsRepository
    {
        Task<Setting?> GetAsync();

        Task<Setting> CreateAsync(Setting setting);

        Task<Setting> UpdateAsync(Setting setting);
    }
}