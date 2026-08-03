using AutoMapper;
using FinSync.Application.Features.Claims.DTOs;
using FinSync.Domain.Entities;

namespace FinSync.Application.Features.Claims.Mapping
{
    public class ClaimMappingProfile : Profile
    {
        public ClaimMappingProfile()
        {
            CreateMap<CreateClaimRequestDto, InsuranceClaim>()
                .ForMember(dest => dest.IncidentDate,
                    opt => opt.MapFrom(src => src.IncidentDate.ToDateTime(TimeOnly.MinValue)))
                .ForMember(dest => dest.ClaimDate,
                    opt => opt.MapFrom(src => src.ClaimDate.ToDateTime(TimeOnly.MinValue)));

            CreateMap<UpdateClaimRequestDto, InsuranceClaim>()
                .ForMember(dest => dest.IncidentDate,
                    opt => opt.MapFrom(src => src.IncidentDate.ToDateTime(TimeOnly.MinValue)))
                .ForMember(dest => dest.ClaimDate,
                    opt => opt.MapFrom(src => src.ClaimDate.ToDateTime(TimeOnly.MinValue)))
                .ForMember(dest => dest.SettlementDate,
                    opt => opt.MapFrom(src => src.SettlementDate.HasValue
                        ? src.SettlementDate.Value.ToDateTime(TimeOnly.MinValue)
                        : (DateTime?)null));

            CreateMap<InsuranceClaim, ClaimResponseDto>()
                .ForMember(dest => dest.IncidentDate,
                    opt => opt.MapFrom(src => DateOnly.FromDateTime(src.IncidentDate)))
                .ForMember(dest => dest.ClaimDate,
                    opt => opt.MapFrom(src => DateOnly.FromDateTime(src.ClaimDate)))
                .ForMember(dest => dest.SettlementDate,
                    opt => opt.MapFrom(src => src.SettlementDate.HasValue
                        ? DateOnly.FromDateTime(src.SettlementDate.Value)
                        : (DateOnly?)null));
        }
    }
}