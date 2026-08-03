using FinSync.Application.Features.Reports.DTOs;
using FinSync.Application.Features.Reports.Interfaces;
using FinSync.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FinSync.Persistence.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly FinSyncDbContext _context;

        public ReportRepository(FinSyncDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ActivePolicyReportDto>> GetActivePoliciesAsync(ReportFilterDto filter)
        {
            var query = _context.Policies
    .Include(p => p.Customer)
    .Include(p => p.Agent)
    .Include(p => p.InsuranceCompany)
    .Include(p => p.InsurancePlan)
    .Where(p => p.PolicyStatus == "Active")
    .AsQueryable();

            if (filter.CompanyId.HasValue)
            {
                query = query.Where(p => p.CompanyId == filter.CompanyId.Value);
            }

            if (filter.AgentId.HasValue)
            {
                query = query.Where(p => p.AgentId == filter.AgentId.Value);
            }

            if (filter.FromDate.HasValue)
            {
                query = query.Where(p => p.StartDate >= filter.FromDate.Value);
            }

            if (filter.ToDate.HasValue)
            {
                query = query.Where(p => p.EndDate <= filter.ToDate.Value);
            }

            query = query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize);

            return await query.Select(p => new ActivePolicyReportDto
            {
                PolicyNumber = p.PolicyNumber,

                CustomerName = p.Customer.FirstName + " " + p.Customer.LastName,

                CompanyName = p.InsuranceCompany.CompanyName,

                PlanName = p.InsurancePlan.PlanName,

                StartDate = p.StartDate,

                EndDate = p.EndDate,

                PremiumAmount = p.PremiumAmount,

                AgentName = p.Agent.FirstName + " " + p.Agent.LastName
            })
            .ToListAsync();
        }

        public async Task<IEnumerable<ExpiringPolicyReportDto>> GetExpiringPoliciesAsync(ReportFilterDto filter)
        {
            throw new NotImplementedException();
        }

        public async Task<PremiumCollectionReportDto> GetPremiumCollectionAsync(ReportFilterDto filter)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AgentPerformanceReportDto>> GetAgentPerformanceAsync(ReportFilterDto filter)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CompanyBusinessReportDto>> GetCompanyBusinessAsync(ReportFilterDto filter)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ClaimReportDto>> GetClaimReportAsync(ReportFilterDto filter)
        {
            throw new NotImplementedException();
        }

        public async Task<DashboardReportDto> GetDashboardReportAsync()
        {
            throw new NotImplementedException();
        }
    }
}