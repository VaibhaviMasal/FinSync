using FinSync.Application.Features.InsurancePlans.DTOs;
using FinSync.Domain.Entities;

namespace FinSync.Application.Features.InsurancePlans.Interfaces;

public interface IInsurancePlanRepository
{
    Task<InsurancePlan> AddAsync(InsurancePlan insurancePlan);

    Task<IEnumerable<InsurancePlan>> GetAllAsync(
        InsurancePlanQueryParametersDto queryParameters);

    Task<InsurancePlan?> GetByIdAsync(int planId);

    Task<InsurancePlan?> UpdateAsync(
        int planId,
        InsurancePlan insurancePlan);

    Task<bool> DeleteAsync(int planId);

    Task<IEnumerable<InsurancePlan>> SearchAsync(string keyword);

    Task<bool> ExistsByPlanCodeAsync(
        string planCode,
        int? excludePlanId = null);

    Task<bool> ExistsByPlanNameAsync(
        string planName,
        int companyId,
        int? excludePlanId = null);
}