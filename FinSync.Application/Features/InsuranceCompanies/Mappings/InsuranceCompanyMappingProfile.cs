using AutoMapper;
using FinSync.Application.Features.InsuranceCompanies.DTOs;
using FinSync.Domain.Entities;

namespace FinSync.Application.Features.InsuranceCompanies.Mappings
{
    public class InsuranceCompanyMappingProfile : Profile
    {
        public InsuranceCompanyMappingProfile()
        {
            CreateMap<CreateInsuranceCompanyRequestDto, InsuranceCompany>();

            CreateMap<UpdateInsuranceCompanyRequestDto, InsuranceCompany>();

            CreateMap<InsuranceCompany, InsuranceCompanyResponseDto>();
        }
    }
}