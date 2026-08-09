using FinSync.Application.Features.Settings.DTOs;

namespace FinSync.Application.Features.Settings.Interfaces
{
    public interface ISettingsService
    {
        Task<SettingsResponseDto> GetAsync();

        Task<SettingsResponseDto> UpdateAsync(UpdateSettingsRequestDto request);
    }
}