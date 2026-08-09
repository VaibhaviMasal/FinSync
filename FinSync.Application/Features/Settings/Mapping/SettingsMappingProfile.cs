using AutoMapper;
using FinSync.Application.Features.Settings.DTOs;
using FinSync.Domain.Entities;

namespace FinSync.Application.Features.Settings.Mapping
{
    public class SettingsMappingProfile : Profile
    {
        public SettingsMappingProfile()
        {
            CreateMap<Setting, SettingsResponseDto>();

            CreateMap<UpdateSettingsRequestDto, Setting>();
        }
    }
}