using FinSync.Application.Features.Claims.DTOs;
using FinSync.Application.Features.Claims.Interfaces;
using FinSync.Domain.Entities;
using FinSync.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FinSync.Persistence.Repositories
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly FinSyncDbContext _context;

        public ClaimRepository(FinSyncDbContext context)
        {
            _context = context;
        }

        public async Task<InsuranceClaim> AddAsync(InsuranceClaim claim)
        {
            await _context.InsuranceClaims.AddAsync(claim);
            await _context.SaveChangesAsync();
            return claim;
        }

        public async Task<InsuranceClaim?> GetByIdAsync(int claimId)
        {
            return await _context.InsuranceClaims
                .Include(c => c.Policy)
                .FirstOrDefaultAsync(c => c.ClaimId == claimId);
        }

        public async Task<IEnumerable<InsuranceClaim>> GetAllAsync(ClaimQueryParametersDto queryParameters)
        {
            var query = _context.InsuranceClaims
                .Include(c => c.Policy)
                .AsQueryable();

            if (queryParameters.ClaimStatus.HasValue)
            {
                query = query.Where(c => c.ClaimStatus == queryParameters.ClaimStatus);
            }

            if (queryParameters.ClaimType.HasValue)
            {
                query = query.Where(c => c.ClaimType == queryParameters.ClaimType);
            }

            query = queryParameters.SortOrder.ToLower() == "desc"
                ? query.OrderByDescending(c => c.ClaimDate)
                : query.OrderBy(c => c.ClaimDate);

            return await query
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize)
                .ToListAsync();
        }

        public async Task<InsuranceClaim?> UpdateAsync(int claimId, InsuranceClaim claim)
        {
            var existingClaim = await _context.InsuranceClaims.FindAsync(claimId);

            if (existingClaim == null)
                return null;

            existingClaim.ClaimType = claim.ClaimType;
            existingClaim.ClaimAmount = claim.ClaimAmount;
            existingClaim.ApprovedAmount = claim.ApprovedAmount;
            existingClaim.IncidentDate = claim.IncidentDate;
            existingClaim.ClaimDate = claim.ClaimDate;
            existingClaim.SettlementDate = claim.SettlementDate;
            existingClaim.ClaimStatus = claim.ClaimStatus;
            existingClaim.Description = claim.Description;
            existingClaim.Remarks = claim.Remarks;
            existingClaim.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return existingClaim;
        }

        public async Task<bool> DeleteAsync(int claimId)
        {
            var claim = await _context.InsuranceClaims.FindAsync(claimId);

            if (claim == null)
                return false;

            _context.InsuranceClaims.Remove(claim);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}