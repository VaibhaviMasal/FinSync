using FinSync.Application.Features.Policies.DTOs;
using FinSync.Application.Features.Policies.Interfaces;
using FinSync.Domain.Entities;
using FinSync.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FinSync.Persistence.Repositories
{
    public class PolicyRepository : IPolicyRepository
    {
        private readonly FinSyncDbContext _context;

        public PolicyRepository(FinSyncDbContext context)
        {
            _context = context;
        }

        // Create
        public async Task<Policy> AddAsync(Policy policy)
        {
            await _context.Policies.AddAsync(policy);
            await _context.SaveChangesAsync();

            return policy;
        }

        // Get All
        public async Task<IEnumerable<Policy>> GetAllAsync(
            PolicyQueryParametersDto queryParameters)
        {
            var query = _context.Policies
                .Include(p => p.Customer)
                .Include(p => p.InsuranceCompany)
                .Include(p => p.InsurancePlan)
                .AsNoTracking()
                .AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(queryParameters.SearchTerm))
            {
                var keyword = queryParameters.SearchTerm.Trim().ToLower();

                query = query.Where(p =>
                    p.PolicyNumber.ToLower().Contains(keyword) ||
                    p.Customer.FirstName.ToLower().Contains(keyword) ||
                    p.Customer.LastName.ToLower().Contains(keyword) ||
                    p.InsuranceCompany.CompanyName.ToLower().Contains(keyword) ||
                    p.InsurancePlan.PlanName.ToLower().Contains(keyword));
            }

            // Filters
            if (queryParameters.CustomerId.HasValue)
            {
                query = query.Where(p =>
                    p.CustomerId == queryParameters.CustomerId.Value);
            }

            if (queryParameters.CompanyId.HasValue)
            {
                query = query.Where(p =>
                    p.CompanyId == queryParameters.CompanyId.Value);
            }

            if (queryParameters.PlanId.HasValue)
            {
                query = query.Where(p =>
                    p.PlanId == queryParameters.PlanId.Value);
            }

            // Sorting
            switch (queryParameters.SortBy?.ToLower())
            {
                case "policynumber":
                    query = queryParameters.IsDescending
                        ? query.OrderByDescending(p => p.PolicyNumber)
                        : query.OrderBy(p => p.PolicyNumber);
                    break;

                case "issuedate":
                    query = queryParameters.IsDescending
                        ? query.OrderByDescending(p => p.IssueDate)
                        : query.OrderBy(p => p.IssueDate);
                    break;

                case "premiumamount":
                    query = queryParameters.IsDescending
                        ? query.OrderByDescending(p => p.PremiumAmount)
                        : query.OrderBy(p => p.PremiumAmount);
                    break;

                default:
                    query = query.OrderBy(p => p.PolicyId);
                    break;
            }

            // Pagination
            query = query
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize);

            return await query.ToListAsync();
        }

        // Get By Id
        public async Task<Policy?> GetByIdAsync(int policyId)
        {
            return await _context.Policies
                .Include(p => p.Customer)
                .Include(p => p.InsuranceCompany)
                .Include(p => p.InsurancePlan)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PolicyId == policyId);
        }

        // Update
        public async Task<Policy?> UpdateAsync(
            int policyId,
            Policy policy)
        {
            var existingPolicy =
                await _context.Policies.FindAsync(policyId);

            if (existingPolicy == null)
                return null;

            existingPolicy.PolicyNumber = policy.PolicyNumber;
            existingPolicy.CustomerId = policy.CustomerId;
            existingPolicy.CompanyId = policy.CompanyId;
            existingPolicy.PlanId = policy.PlanId;
            existingPolicy.IssueDate = policy.IssueDate;
            existingPolicy.StartDate = policy.StartDate;
            existingPolicy.EndDate = policy.EndDate;
            existingPolicy.PremiumAmount = policy.PremiumAmount;
            existingPolicy.PremiumFrequency = policy.PremiumFrequency;
            existingPolicy.SumAssured = policy.SumAssured;
            existingPolicy.PolicyStatus = policy.PolicyStatus;
            existingPolicy.NomineeName = policy.NomineeName;
            existingPolicy.NomineeRelation = policy.NomineeRelation;
            existingPolicy.NomineePhoneNumber = policy.NomineePhoneNumber;
            existingPolicy.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return existingPolicy;
        }

        // Delete
        public async Task<bool> DeleteAsync(int policyId)
        {
            var policy =
                await _context.Policies.FindAsync(policyId);

            if (policy == null)
                return false;

            _context.Policies.Remove(policy);

            await _context.SaveChangesAsync();

            return true;
        }

        // Search
        public async Task<IEnumerable<Policy>> SearchAsync(string keyword)
        {
            keyword = keyword.Trim().ToLower();

            return await _context.Policies
                .Include(p => p.Customer)
                .Include(p => p.InsuranceCompany)
                .Include(p => p.InsurancePlan)
                .AsNoTracking()
                .Where(p =>
                    p.PolicyNumber.ToLower().Contains(keyword) ||
                    p.Customer.FirstName.ToLower().Contains(keyword) ||
                    p.Customer.LastName.ToLower().Contains(keyword) ||
                    p.InsuranceCompany.CompanyName.ToLower().Contains(keyword) ||
                    p.InsurancePlan.PlanName.ToLower().Contains(keyword))
                .ToListAsync();
        }

        // Duplicate Policy Number
        public async Task<bool> ExistsByPolicyNumberAsync(
            string policyNumber,
            int? excludePolicyId = null)
        {
            policyNumber = policyNumber.Trim().ToLower();

            return await _context.Policies.AnyAsync(p =>
                p.PolicyNumber.ToLower() == policyNumber &&
                (!excludePolicyId.HasValue || p.PolicyId != excludePolicyId.Value));
        }
    }
}