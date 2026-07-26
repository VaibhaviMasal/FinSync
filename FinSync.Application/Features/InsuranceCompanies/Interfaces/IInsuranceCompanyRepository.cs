using FinSync.Application.Features.InsuranceCompanies.DTOs;
using FinSync.Domain.Entities;

namespace FinSync.Application.Features.InsuranceCompanies.Interfaces
{
    public interface IInsuranceCompanyRepository
    {
        Task<InsuranceCompany> AddAsync(InsuranceCompany company);

        Task<IEnumerable<InsuranceCompany>> GetAllAsync(
            InsuranceCompanyQueryParametersDto queryParameters);

        Task<InsuranceCompany?> GetByIdAsync(int companyId);

        Task<InsuranceCompany?> UpdateAsync(
            int companyId,
            InsuranceCompany company);

        Task<bool> DeleteAsync(int companyId);

        Task<IEnumerable<InsuranceCompany>> SearchAsync(string keyword);

        Task<bool> ExistsByCompanyCodeAsync(
            string companyCode,
            int? excludeCompanyId = null);

        Task<bool> ExistsByCompanyNameAsync(
            string companyName,
            int? excludeCompanyId = null);
    }
}