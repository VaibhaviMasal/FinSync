using FinSync.Application.Features.InsurancePlans.DTOs;
using FinSync.Application.Features.InsurancePlans.Interfaces;
using FinSync.Domain.Entities;
using FinSync.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FinSync.Persistence.Repositories
{
    public class InsurancePlanRepository : IInsurancePlanRepository
    {
        private readonly FinSyncDbContext _context;

        public InsurancePlanRepository(FinSyncDbContext context)
        {
            _context = context;
        }

        // Create
        public async Task<InsurancePlan> AddAsync(InsurancePlan insurancePlan)
        {
            await _context.InsurancePlans.AddAsync(insurancePlan);
            await _context.SaveChangesAsync();

            return insurancePlan;
        }

        // Get All
        public async Task<IEnumerable<InsurancePlan>> GetAllAsync(
            InsurancePlanQueryParametersDto queryParameters)
        {
            var query = _context.InsurancePlans
                .Include(p => p.InsuranceCompany)
                .AsNoTracking()
                .AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(queryParameters.SearchTerm))
            {
                var keyword = queryParameters.SearchTerm.Trim().ToLower();

                query = query.Where(p =>
                    p.PlanName.ToLower().Contains(keyword) ||
                    p.PlanCode.ToLower().Contains(keyword) ||
                    p.PlanType.ToLower().Contains(keyword));
            }

            // Filter by Company
            if (queryParameters.CompanyId.HasValue)
            {
                query = query.Where(p =>
                    p.CompanyId == queryParameters.CompanyId.Value);
            }

            // Sorting
            switch (queryParameters.SortBy?.ToLower())
            {
                case "planname":
                    query = queryParameters.IsDescending
                        ? query.OrderByDescending(p => p.PlanName)
                        : query.OrderBy(p => p.PlanName);
                    break;

                case "plancode":
                    query = queryParameters.IsDescending
                        ? query.OrderByDescending(p => p.PlanCode)
                        : query.OrderBy(p => p.PlanCode);
                    break;

                case "plantype":
                    query = queryParameters.IsDescending
                        ? query.OrderByDescending(p => p.PlanType)
                        : query.OrderBy(p => p.PlanType);
                    break;

                default:
                    query = query.OrderBy(p => p.PlanId);
                    break;
            }

            // Pagination
            query = query
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize);

            return await query.ToListAsync();
        }

        // Get By Id
        public async Task<InsurancePlan?> GetByIdAsync(int planId)
        {
            return await _context.InsurancePlans
                .Include(p => p.InsuranceCompany)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PlanId == planId);
        }

        // Update
        public async Task<InsurancePlan?> UpdateAsync(
            int planId,
            InsurancePlan insurancePlan)
        {
            var existingPlan =
                await _context.InsurancePlans.FindAsync(planId);

            if (existingPlan == null)
                return null;

            existingPlan.CompanyId = insurancePlan.CompanyId;
            existingPlan.PlanName = insurancePlan.PlanName;
            existingPlan.PlanCode = insurancePlan.PlanCode;
            existingPlan.PlanType = insurancePlan.PlanType;
            existingPlan.Description = insurancePlan.Description;
            existingPlan.MinimumAge = insurancePlan.MinimumAge;
            existingPlan.MaximumAge = insurancePlan.MaximumAge;
            existingPlan.PolicyTerm = insurancePlan.PolicyTerm;
            existingPlan.PremiumFrequency = insurancePlan.PremiumFrequency;
            existingPlan.MinimumSumAssured = insurancePlan.MinimumSumAssured;
            existingPlan.MaximumSumAssured = insurancePlan.MaximumSumAssured;
            existingPlan.IsActive = insurancePlan.IsActive;
            existingPlan.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return existingPlan;
        }

        // Delete
        public async Task<bool> DeleteAsync(int planId)
        {
            var plan =
                await _context.InsurancePlans.FindAsync(planId);

            if (plan == null)
                return false;

            _context.InsurancePlans.Remove(plan);

            await _context.SaveChangesAsync();

            return true;
        }

        // Search
        public async Task<IEnumerable<InsurancePlan>> SearchAsync(string keyword)
        {
            keyword = keyword.Trim().ToLower();

            return await _context.InsurancePlans
                .Include(p => p.InsuranceCompany)
                .AsNoTracking()
                .Where(p =>
                    p.PlanName.ToLower().Contains(keyword) ||
                    p.PlanCode.ToLower().Contains(keyword) ||
                    p.PlanType.ToLower().Contains(keyword))
                .ToListAsync();
        }

        // Duplicate Plan Code
        public async Task<bool> ExistsByPlanCodeAsync(
            string planCode,
            int? excludePlanId = null)
        {
            planCode = planCode.Trim().ToLower();

            return await _context.InsurancePlans.AnyAsync(p =>
                p.PlanCode.ToLower() == planCode &&
                (!excludePlanId.HasValue || p.PlanId != excludePlanId.Value));
        }

        // Duplicate Plan Name (within same company)
        public async Task<bool> ExistsByPlanNameAsync(
            string planName,
            int companyId,
            int? excludePlanId = null)
        {
            planName = planName.Trim().ToLower();

            return await _context.InsurancePlans.AnyAsync(p =>
                p.CompanyId == companyId &&
                p.PlanName.ToLower() == planName &&
                (!excludePlanId.HasValue || p.PlanId != excludePlanId.Value));
        }
    }
}