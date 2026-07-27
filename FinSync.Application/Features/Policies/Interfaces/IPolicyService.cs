using FinSync.Application.Features.Policies.DTOs;

namespace FinSync.Application.Features.Policies.Interfaces
{
    public interface IPolicyService
    {
        Task<PolicyResponseDto> CreatePolicyAsync(
            CreatePolicyRequestDto request);

        Task<IEnumerable<PolicyResponseDto>> GetAllAsync(
            PolicyQueryParametersDto queryParameters);

        Task<PolicyResponseDto> GetByIdAsync(int policyId);

        Task<PolicyResponseDto> UpdatePolicyAsync(
            int policyId,
            UpdatePolicyRequestDto request);

        Task<bool> DeletePolicyAsync(int policyId);

        Task<IEnumerable<PolicyResponseDto>> SearchAsync(
            string keyword);
    }
}