using FinSync.Application.Features.PolicyRenewals.DTOs;
using FinSync.Domain.Entities;

namespace FinSync.Application.Features.PolicyRenewals.Interfaces
{
    public interface IPolicyRenewalRepository
    {
        Task<PolicyRenewal> AddAsync(PolicyRenewal renewal);

        Task<IEnumerable<PolicyRenewal>> GetAllAsync(
            PolicyRenewalQueryParametersDto queryParameters);

        Task<PolicyRenewal?> GetByIdAsync(int renewalId);

        Task<PolicyRenewal?> UpdateAsync(
            int renewalId,
            PolicyRenewal renewal);

        Task<bool> DeleteAsync(int renewalId);

        Task<IEnumerable<PolicyRenewal>> SearchAsync(string keyword);

        Task<IEnumerable<PolicyRenewal>> GetByPolicyIdAsync(int policyId);

        Task<IEnumerable<PolicyRenewal>> GetUpcomingRenewalsAsync(int days = 30);

        Task<IEnumerable<PolicyRenewal>> GetExpiredRenewalsAsync();

        Task SaveChangesAsync();
    }
}