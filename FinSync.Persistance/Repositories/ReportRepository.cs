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

        private IQueryable<Policy> GetPolicyReportQuery()
        {
            return _context.Policies
                .Include(p => p.Customer)
                .Include(p => p.Agent)
                .Include(p => p.InsuranceCompany)
                .Include(p => p.InsurancePlan);
        }

        // ===========================================================
        // Active Policies Report
        // ===========================================================
        public async Task<IEnumerable<ActivePolicyReportDto>> GetActivePoliciesAsync(ReportFilterDto filter)
        {
            var query = GetPolicyReportQuery()
                .Where(p => p.PolicyStatus == "Active");

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

            query = filter.SortOrder.ToLower() == "desc"
                ? query.OrderByDescending(p => p.PolicyNumber)
                : query.OrderBy(p => p.PolicyNumber);

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

        // ===========================================================
        // Expiring Policies Report
        // ===========================================================
        public async Task<IEnumerable<ExpiringPolicyReportDto>> GetExpiringPoliciesAsync(ReportFilterDto filter)
        {
            var today = DateTime.Today;
            var next30Days = today.AddDays(30);

            var query = GetPolicyReportQuery()
                .Where(p => p.EndDate >= today && p.EndDate <= next30Days);

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
                query = query.Where(p => p.EndDate >= filter.FromDate.Value);
            }

            if (filter.ToDate.HasValue)
            {
                query = query.Where(p => p.EndDate <= filter.ToDate.Value);
            }

            query = query
                .OrderBy(p => p.EndDate)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize);

            return query
                .AsEnumerable()
                .Select(p => new ExpiringPolicyReportDto
                {
                    PolicyNumber = p.PolicyNumber,
                    CustomerName = p.Customer.FirstName + " " + p.Customer.LastName,
                    CompanyName = p.InsuranceCompany.CompanyName,
                    ExpiryDate = p.EndDate,
                    DaysRemaining = (p.EndDate.Date - today).Days,
                    PremiumAmount = p.PremiumAmount,
                    AgentName = p.Agent.FirstName + " " + p.Agent.LastName
                })
                .ToList();
        }

        // ===========================================================
        // Remaining Reports
        // ===========================================================

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

            var totalPayments = await payments.CountAsync();

            var paidPayments = await payments
                .Where(p => p.PaymentStatus == Domain.Enums.PaymentStatus.Paid)
                .CountAsync();

            var pendingPayments = await payments
                .Where(p => p.PaymentStatus == Domain.Enums.PaymentStatus.Pending)
                .CountAsync();

            var totalPremiumCollected = await payments
                .Where(p => p.PaymentStatus == Domain.Enums.PaymentStatus.Paid)
                .SumAsync(p => (decimal?)p.Amount) ?? 0;

            var pendingPremium = await payments
                .Where(p => p.PaymentStatus == Domain.Enums.PaymentStatus.Pending)
                .SumAsync(p => (decimal?)p.Amount) ?? 0;

            return new PremiumCollectionReportDto
            {
                TotalPremiumCollected = totalPremiumCollected,
                PaidPremium = totalPremiumCollected,
                PendingPremium = pendingPremium,
                TotalPayments = totalPayments,
                PaidPayments = paidPayments,
                PendingPayments = pendingPayments
            };
        }

        public async Task<IEnumerable<AgentPerformanceReportDto>> GetAgentPerformanceAsync(ReportFilterDto filter)
        {
            return await _context.Agents
                .Select(agent => new AgentPerformanceReportDto
                {
                    AgentId = agent.AgentId,

                    AgentName = agent.FirstName + " " + agent.LastName,

                    PoliciesSold = agent.Policies.Count(),

                    TotalBusiness = agent.Policies.Sum(p => (decimal?)p.PremiumAmount) ?? 0,

                    ActivePolicies = agent.Policies.Count(p => p.PolicyStatus == "Active"),

                    Renewals = agent.Policies.Sum(p => p.PolicyRenewals.Count),

                    ClaimsHandled = agent.Policies.Sum(p => p.InsuranceClaims.Count)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<CompanyBusinessReportDto>> GetCompanyBusinessAsync(ReportFilterDto filter)
        {
            return await _context.InsuranceCompanies
                .Select(company => new CompanyBusinessReportDto
                {
                    CompanyId = company.CompanyId,

                    CompanyName = company.CompanyName,

                    TotalPolicies = company.Policies.Count(),

                    TotalCustomers = company.Policies
                        .Select(p => p.CustomerId)
                        .Distinct()
                        .Count(),

                    TotalPremium = company.Policies
                        .Sum(p => (decimal?)p.PremiumAmount) ?? 0,

                    TotalClaims = company.Policies
                        .SelectMany(p => p.InsuranceClaims)
                        .Count()
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ClaimReportDto>> GetClaimReportAsync(ReportFilterDto filter)
        {
            var query = _context.InsuranceClaims
                .Include(c => c.Policy)
                    .ThenInclude(p => p.Customer)
                .AsQueryable();

            if (filter.FromDate.HasValue)
            {
                query = query.Where(c => c.ClaimDate >= filter.FromDate.Value);
            }

            if (filter.ToDate.HasValue)
            {
                query = query.Where(c => c.ClaimDate <= filter.ToDate.Value);
            }

            return await query.Select(c => new ClaimReportDto
            {
                ClaimNumber = c.ClaimNumber,

                PolicyNumber = c.Policy.PolicyNumber,

                CustomerName = c.Policy.Customer.FirstName + " " + c.Policy.Customer.LastName,

                ClaimAmount = c.ClaimAmount,

                ApprovedAmount = c.ApprovedAmount,

                ClaimStatus = c.ClaimStatus,

                ClaimDate = c.ClaimDate,

                SettlementDate = c.SettlementDate
            })
            .ToListAsync();
        }

        public async Task<DashboardReportDto> GetDashboardReportAsync()
        {
            return new DashboardReportDto
            {
                TotalCustomers = await _context.Customers.CountAsync(),

                TotalAgents = await _context.Agents.CountAsync(),

                TotalPolicies = await _context.Policies.CountAsync(),

                ActivePolicies = await _context.Policies
                    .CountAsync(p => p.PolicyStatus == "Active"),

                ExpiringPolicies = await _context.Policies
                    .CountAsync(p =>
                        p.EndDate >= DateTime.Today &&
                        p.EndDate <= DateTime.Today.AddDays(30)),

                PendingClaims = await _context.InsuranceClaims
                    .CountAsync(c => c.ClaimStatus == Domain.Enums.ClaimStatus.Pending),

                PremiumCollected = await _context.PremiumPayments
                    .Where(p => p.PaymentStatus == Domain.Enums.PaymentStatus.Paid)
                    .SumAsync(p => (decimal?)p.Amount) ?? 0,

                TotalBusiness = await _context.Policies
                    .SumAsync(p => (decimal?)p.PremiumAmount) ?? 0
            };
        }
    }
}