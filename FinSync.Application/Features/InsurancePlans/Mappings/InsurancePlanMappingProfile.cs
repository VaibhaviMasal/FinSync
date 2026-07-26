using AutoMapper;
using FinSync.Application.Features.InsurancePlans.DTOs;
using FinSync.Domain.Entities;

namespace FinSync.Application.Features.InsurancePlans.Mappings
{
    public class InsurancePlanMappingProfile : Profile
    {
        public InsurancePlanMappingProfile()
        {
            CreateMap<CreateInsurancePlanRequestDto, InsurancePlan>();

            CreateMap<UpdateInsurancePlanRequestDto, InsurancePlan>();

            CreateMap<InsurancePlan, InsurancePlanResponseDto>()
                .ForMember(
                    dest => dest.CompanyName,
                    opt => opt.MapFrom(src => src.InsuranceCompany.CompanyName));
        }
    }
}