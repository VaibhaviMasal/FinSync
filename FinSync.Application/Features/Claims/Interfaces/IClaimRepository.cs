using FinSync.Application.Features.Claims.DTOs;
using FinSync.Domain.Entities;

namespace FinSync.Application.Features.Claims.Interfaces
{
    public interface IClaimRepository
    {
        Task<InsuranceClaim> AddAsync(InsuranceClaim claim);

        Task<InsuranceClaim?> GetByIdAsync(int claimId);

        Task<IEnumerable<InsuranceClaim>> GetAllAsync(ClaimQueryParametersDto queryParameters);

        Task<InsuranceClaim?> UpdateAsync(int claimId, InsuranceClaim claim);

        Task<bool> DeleteAsync(int claimId);
    }
}