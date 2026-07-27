using AutoMapper;
using FinSync.Application.Features.Policies.DTOs;
using FinSync.Domain.Entities;

namespace FinSync.Application.Features.Policies.Mappings
{
    public class PolicyMappingProfile : Profile
    {
        public PolicyMappingProfile()
        {
            CreateMap<CreatePolicyRequestDto, Policy>();

            CreateMap<UpdatePolicyRequestDto, Policy>();

            CreateMap<Policy, PolicyResponseDto>()
                .ForMember(
                    dest => dest.CustomerName,
                    opt => opt.MapFrom(src =>
                        src.Customer.FirstName + " " + src.Customer.LastName))

                .ForMember(
                    dest => dest.CompanyName,
                    opt => opt.MapFrom(src =>
                        src.InsuranceCompany.CompanyName))

                .ForMember(
                    dest => dest.PlanName,
                    opt => opt.MapFrom(src =>
                        src.InsurancePlan.PlanName));
        }
    }
}