using FinSync.Application.Features.Reports.DTOs;
using FinSync.Application.Features.Reports.Interfaces;
using FinSync.Domain.Entities;
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

        public Task<IEnumerable<ActivePolicyReportDto>> GetActivePoliciesAsync(ReportFilterDto filter)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AgentPerformanceReportDto>> GetAgentPerformanceAsync(ReportFilterDto filter)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ClaimReportDto>> GetClaimReportAsync(ReportFilterDto filter)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CompanyBusinessReportDto>> GetCompanyBusinessAsync(ReportFilterDto filter)
        {
            throw new NotImplementedException();
        }

        public Task<DashboardReportDto> GetDashboardReportAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ExpiringPolicyReportDto>> GetExpiringPoliciesAsync(ReportFilterDto filter)
        {
            throw new NotImplementedException();
        }

        public Task<PremiumCollectionReportDto> GetPremiumCollectionAsync(ReportFilterDto filter)
        {
            throw new NotImplementedException();
        }

        private IQueryable<Policy> GetPolicyReportQuery()
        {
            return _context.Policies
                .Include(p => p.Customer)
                .Include(p => p.Agent)
                .Include(p => p.InsuranceCompany)
                .Include(p => p.InsurancePlan);
        }

        // Report methods will be added here...
    }
}