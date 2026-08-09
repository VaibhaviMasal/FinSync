using AutoMapper;
using FinSync.Application.Features.Settings.DTOs;
using FinSync.Application.Features.Settings.Interfaces;
using FinSync.Domain.Entities;

namespace FinSync.Application.Features.Settings.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsRepository _repository;
        private readonly IMapper _mapper;

        public SettingsService(
            ISettingsRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SettingsResponseDto> GetAsync()
        {
            var setting = await _repository.GetAsync();

            if (setting == null)
            {
                // First time setup
                var defaultSetting = new Setting
                {
                    BusinessName = "My Business",
                    OwnerName = "Owner",
                    Currency = "INR"
                };

                var created = await _repository.CreateAsync(defaultSetting);

                return _mapper.Map<SettingsResponseDto>(created);
            }

            return _mapper.Map<SettingsResponseDto>(setting);
        }

        public async Task<SettingsResponseDto> UpdateAsync(UpdateSettingsRequestDto request)
        {
            var existing = await _repository.GetAsync();

            if (existing == null)
            {
                var newSetting = _mapper.Map<Setting>(request);

                var created = await _repository.CreateAsync(newSetting);

                return _mapper.Map<SettingsResponseDto>(created);
            }

            _mapper.Map(request, existing);

            existing.UpdatedDate = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(existing);

            return _mapper.Map<SettingsResponseDto>(updated);
        }
    }
}