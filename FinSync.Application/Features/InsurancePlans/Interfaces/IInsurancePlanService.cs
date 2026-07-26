using FinSync.Application.Features.InsurancePlans.DTOs;

namespace FinSync.Application.Features.InsurancePlans.Interfaces
{
    public interface IInsurancePlanService
    {
        Task<InsurancePlanResponseDto> CreatePlanAsync(
            CreateInsurancePlanRequestDto request);

        Task<IEnumerable<InsurancePlanResponseDto>> GetAllAsync(
            InsurancePlanQueryParametersDto queryParameters);

        Task<InsurancePlanResponseDto> GetByIdAsync(int planId);

        Task<InsurancePlanResponseDto> UpdatePlanAsync(
            int planId,
            UpdateInsurancePlanRequestDto request);

        Task<bool> DeletePlanAsync(int planId);

        Task<IEnumerable<InsurancePlanResponseDto>> SearchAsync(
            string keyword);
    }
}