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
            var today = DateTime.Today;
            var next30Days = today.AddDays(30);

            var query = _context.Policies
                .Include(p => p.Customer)
                .Include(p => p.Agent)
                .Include(p => p.InsuranceCompany)
                .Include(p => p.InsurancePlan)
                .Where(p => p.EndDate >= today && p.EndDate <= next30Days)
                .AsQueryable();

            if (filter.CompanyId.HasValue)
            {
                query = query.Where(p => p.CompanyId == filter.CompanyId.Value);
            }

            if (filter.AgentId.HasValue)
            {
                query = query.Where(p => p.AgentId == filter.AgentId.Value);
            }

            query = query
                .OrderBy(p => p.EndDate)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize);

            return await query.Select(p => new ExpiringPolicyReportDto
            {
                PolicyNumber = p.PolicyNumber,

                CustomerName = p.Customer.FirstName + " " + p.Customer.LastName,

                CompanyName = p.InsuranceCompany.CompanyName,

                ExpiryDate = p.EndDate,

                DaysRemaining = (p.EndDate.Date - today).Days,

                PremiumAmount = p.PremiumAmount,

                AgentName = p.Agent.FirstName + " " + p.Agent.LastName

            }).ToListAsync();
        }

        public async Task<PremiumCollectionReportDto> GetPremiumCollectionAsync(ReportFilterDto filter)
        {
            var payments = _context.PremiumPayments.AsQueryable();

            if (filter.FromDate.HasValue)
            {
                payments = payments.Where(p => p.CreatedDate >= filter.FromDate.Value);
            }

            if (filter.ToDate.HasValue)
            {
                payments = payments.Where(p => p.CreatedDate <= filter.ToDate.Value);
            }

            var totalPremium = await payments.SumAsync(x => x.Amount);

            var paidPremium = await payments
                .Where(x => x.PaymentStatus == Domain.Enums.PaymentStatus.Paid)
                .SumAsync(x => x.Amount);

            var pendingPremium = await payments
                .Where(x => x.PaymentStatus == Domain.Enums.PaymentStatus.Pending)
                .SumAsync(x => x.Amount);

            return new PremiumCollectionReportDto
            {
                TotalPremiumCollected = totalPremium,
                PaidPremium = paidPremium,
                PendingPremium = pendingPremium,

                TotalPayments = await payments.CountAsync(),

                PaidPayments = await payments.CountAsync(x => x.PaymentStatus == Domain.Enums.PaymentStatus.Paid),

                PendingPayments = await payments.CountAsync(x => x.PaymentStatus == Domain.Enums.PaymentStatus.Pending)
            };
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