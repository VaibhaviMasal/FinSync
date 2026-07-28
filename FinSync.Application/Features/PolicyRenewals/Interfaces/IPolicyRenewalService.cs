using FinSync.Application.Features.PolicyRenewals.DTOs;

namespace FinSync.Application.Features.PolicyRenewals.Interfaces
{
    public interface IPolicyRenewalService
    {
        Task<PolicyRenewalResponseDto> CreatePolicyRenewalAsync(
            CreatePolicyRenewalRequestDto request);

        Task<IEnumerable<PolicyRenewalResponseDto>> GetAllAsync(
            PolicyRenewalQueryParametersDto queryParameters);

        Task<PolicyRenewalResponseDto> GetByIdAsync(int renewalId);

        Task<PolicyRenewalResponseDto> UpdatePolicyRenewalAsync(
            int renewalId,
            UpdatePolicyRenewalRequestDto request);

        Task<bool> DeletePolicyRenewalAsync(int renewalId);

        Task<IEnumerable<PolicyRenewalResponseDto>> SearchAsync(string keyword);

        Task<IEnumerable<PolicyRenewalResponseDto>> GetByPolicyIdAsync(int policyId);

        Task<IEnumerable<PolicyRenewalResponseDto>> GetUpcomingRenewalsAsync(int days = 30);

        Task<IEnumerable<PolicyRenewalResponseDto>> GetExpiredRenewalsAsync();
    }
}