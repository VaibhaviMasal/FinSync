using FinSync.Application.Features.Policies.DTOs;
using FinSync.Domain.Entities;

namespace FinSync.Application.Features.Policies.Interfaces
{
    public interface IPolicyRepository
    {
        Task<Policy> AddAsync(Policy policy);

        Task<IEnumerable<Policy>> GetAllAsync(
            PolicyQueryParametersDto queryParameters);

        Task<Policy?> GetByIdAsync(int policyId);

        Task<Policy?> UpdateAsync(
            int policyId,
            Policy policy);

        Task<bool> DeleteAsync(int policyId);

        Task<IEnumerable<Policy>> SearchAsync(string keyword);

        Task<bool> ExistsByPolicyNumberAsync(
            string policyNumber,
            int? excludePolicyId = null);
    }
}