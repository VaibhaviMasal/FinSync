using AutoMapper;
using FinSync.Application.Features.PolicyRenewals.DTOs;
using FinSync.Domain.Entities;

namespace FinSync.Application.Features.PolicyRenewals.Mapping
{
    public class PolicyRenewalMappingProfile : Profile
    {
        public PolicyRenewalMappingProfile()
        {
            CreateMap<CreatePolicyRenewalRequestDto, PolicyRenewal>();

            CreateMap<UpdatePolicyRenewalRequestDto, PolicyRenewal>();

            CreateMap<PolicyRenewal, PolicyRenewalResponseDto>()
                .ForMember(
                    dest => dest.PolicyNumber,
                    opt => opt.MapFrom(src => src.Policy.PolicyNumber));
        }
    }
}