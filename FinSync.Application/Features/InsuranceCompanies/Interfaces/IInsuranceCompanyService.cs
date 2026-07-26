using FinSync.Application.Features.InsuranceCompanies.DTOs;

namespace FinSync.Application.Features.InsuranceCompanies.Interfaces
{
    public interface IInsuranceCompanyService
    {
        Task<InsuranceCompanyResponseDto> CreateCompanyAsync(CreateInsuranceCompanyRequestDto request);

        Task<IEnumerable<InsuranceCompanyResponseDto>> GetAllAsync(
            InsuranceCompanyQueryParametersDto queryParameters);

        Task<InsuranceCompanyResponseDto> GetByIdAsync(int companyId);

        Task<InsuranceCompanyResponseDto> UpdateCompanyAsync(
            int companyId,
            UpdateInsuranceCompanyRequestDto request);

        Task<bool> DeleteCompanyAsync(int companyId);

        Task<IEnumerable<InsuranceCompanyResponseDto>> SearchAsync(string keyword);
    }
}