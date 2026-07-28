using FinSync.Application.Features.PolicyRenewals.DTOs;
using FinSync.Application.Features.PolicyRenewals.Interfaces;
using FinSync.Domain.Entities;
using FinSync.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FinSync.Persistence.Repositories
{
    public class PolicyRenewalRepository : IPolicyRenewalRepository
    {
        private readonly FinSyncDbContext _context;

        public PolicyRenewalRepository(FinSyncDbContext context)
        {
            _context = context;
        }

        public async Task<PolicyRenewal> AddAsync(PolicyRenewal renewal)
        {
            await _context.PolicyRenewals.AddAsync(renewal);
            await _context.SaveChangesAsync();

            return renewal;
        }

        public async Task<IEnumerable<PolicyRenewal>> GetAllAsync(
            PolicyRenewalQueryParametersDto queryParameters)
        {
            var query = _context.PolicyRenewals
                .Include(x => x.Policy)
                .AsQueryable();

            // Filter by Renewal Status
            if (!string.IsNullOrWhiteSpace(queryParameters.Status))
            {
                query = query.Where(x =>
                    x.RenewalStatus.ToString() == queryParameters.Status);
            }

            // Sorting
            query = queryParameters.SortBy?.ToLower() switch
            {
                "renewaldate" => queryParameters.Descending
                    ? query.OrderByDescending(x => x.RenewalDate)
                    : query.OrderBy(x => x.RenewalDate),

                "premium" => queryParameters.Descending
                    ? query.OrderByDescending(x => x.RenewalPremium)
                    : query.OrderBy(x => x.RenewalPremium),

                _ => queryParameters.Descending
                    ? query.OrderByDescending(x => x.CreatedDate)
                    : query.OrderBy(x => x.CreatedDate)
            };

            // Pagination
            query = query
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize);

            return await query.ToListAsync();
        }

        public async Task<PolicyRenewal?> GetByIdAsync(int renewalId)
        {
            return await _context.PolicyRenewals
                .Include(x => x.Policy)
                .FirstOrDefaultAsync(x => x.RenewalId == renewalId);
        }

        public async Task<PolicyRenewal?> UpdateAsync(
            int renewalId,
            PolicyRenewal renewal)
        {
            var existingRenewal = await _context.PolicyRenewals
                .FirstOrDefaultAsync(x => x.RenewalId == renewalId);

            if (existingRenewal == null)
                return null;

            _context.Entry(existingRenewal)
                .CurrentValues
                .SetValues(renewal);

            existingRenewal.RenewalId = renewalId;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(renewalId);
        }

        public async Task<bool> DeleteAsync(int renewalId)
        {
            var renewal = await _context.PolicyRenewals
                .FirstOrDefaultAsync(x => x.RenewalId == renewalId);

            if (renewal == null)
                return false;

            _context.PolicyRenewals.Remove(renewal);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<PolicyRenewal>> SearchAsync(string keyword)
        {
            return await _context.PolicyRenewals
                .Include(x => x.Policy)
                .Where(x =>
                    x.Policy.PolicyNumber.Contains(keyword))
                .ToListAsync();
        }

        public async Task<IEnumerable<PolicyRenewal>> GetByPolicyIdAsync(int policyId)
        {
            return await _context.PolicyRenewals
                .Include(x => x.Policy)
                .Where(x => x.PolicyId == policyId)
                .ToListAsync();
        }

        public async Task<IEnumerable<PolicyRenewal>> GetUpcomingRenewalsAsync(int days = 30)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var targetDate = today.AddDays(days);

            return await _context.PolicyRenewals
                .Include(x => x.Policy)
                .Where(x =>
                    x.NewExpiryDate >= today &&
                    x.NewExpiryDate <= targetDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<PolicyRenewal>> GetExpiredRenewalsAsync()
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            return await _context.PolicyRenewals
                .Include(x => x.Policy)
                .Where(x =>
                    x.NewExpiryDate < today)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}