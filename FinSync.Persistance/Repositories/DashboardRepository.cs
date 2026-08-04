using FinSync.Application.Features.Dashboard.DTOs;
using FinSync.Application.Features.Dashboard.Interfaces;
using FinSync.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FinSync.Persistence.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly FinSyncDbContext _context;

        public DashboardRepository(FinSyncDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardSummaryDto> GetDashboardSummaryAsync()
        {
            var today = DateTime.UtcNow.Date;
            var next30Days = today.AddDays(30);

            return new DashboardSummaryDto
            {
                TotalCustomers = await _context.Customers.CountAsync(),

                TotalAgents = await _context.Agents.CountAsync(),

                TotalInsuranceCompanies = await _context.InsuranceCompanies.CountAsync(),

                TotalPlans = await _context.InsurancePlans.CountAsync(),

                ActivePolicies = await _context.Policies
                   .CountAsync(p => p.PolicyStatus == "Active"),

                ExpiredPolicies = await _context.Policies
                    .CountAsync(p => p.EndDate < today),

                PoliciesExpiringSoon = await _context.Policies
                    .CountAsync(p => p.EndDate >= today &&
                                     p.EndDate <= next30Days),

                TotalPremiumCollected = await _context.PremiumPayments
                    .SumAsync(p => (decimal?)p.Amount) ?? 0,

                PremiumsPaid = await _context.PremiumPayments
                    .CountAsync(),

                PremiumsPending = await _context.PremiumPayments
                   .CountAsync(p => p.PaymentStatus == Domain.Enums.PaymentStatus.Pending)
            };
        }

        public async Task<IEnumerable<DashboardAlertDto>> GetDashboardAlertsAsync()
        {
            var today = DateTime.Today;
            var next30Days = today.AddDays(30);

            var policies = await _context.Policies
                .Include(p => p.Customer)
                .Include(p => p.InsuranceCompany)
                .Where(p => p.EndDate >= today && p.EndDate <= next30Days)
                .OrderBy(p => p.EndDate)
                .ToListAsync();

            return policies.Select(p => new DashboardAlertDto
            {
                PolicyId = p.PolicyId,
                PolicyNumber = p.PolicyNumber,
                CustomerName = $"{p.Customer.FirstName} {p.Customer.LastName}",
                CompanyName = p.InsuranceCompany.CompanyName,
                ExpiryDate = p.EndDate,
                DaysRemaining = (p.EndDate.Date - today).Days
            }).ToList();
        }

        public async Task<DashboardRecentActivityDto> GetRecentActivityAsync()
        {
            var recentCustomers = await _context.Customers
                .OrderByDescending(c => c.CreatedDate)
                .Take(5)
                .Select(c => new RecentCustomerDto
                {
                    CustomerId = c.CustomerId,
                    FullName = c.FirstName + " " + c.LastName,
                    CreatedDate = c.CreatedDate
                })
                .ToListAsync();

            var recentPolicies = await _context.Policies
                .Include(p => p.Customer)
                .Include(p => p.InsuranceCompany)
                .OrderByDescending(p => p.CreatedDate)
                .Take(5)
                .Select(p => new RecentPolicyDto
                {
                    PolicyId = p.PolicyId,
                    PolicyNumber = p.PolicyNumber,
                    CustomerName = p.Customer.FirstName + " " + p.Customer.LastName,
                    IssueDate = p.CreatedDate,
                    CompanyName = p.InsuranceCompany.CompanyName
                })
                .ToListAsync();

            var recentPayments = await _context.PremiumPayments
                 .OrderByDescending(p => p.CreatedDate)
                 .Take(5)
                 .Select(p => new RecentPaymentDto
                 {
                    PremiumPaymentId = p.PremiumPaymentId,
                    Amount = p.Amount,
                    DueDate = p.DueDate,
                    PaymentStatus = p.PaymentStatus
                 })
                .ToListAsync();

            return new DashboardRecentActivityDto
            {
                RecentCustomers = recentCustomers,
                RecentPolicies = recentPolicies,
                RecentPayments = recentPayments
            };
        }

        public async Task<DashboardAnalyticsDto> GetDashboardAnalyticsAsync()
        {
            var analytics = new DashboardAnalyticsDto();

            // Policies By Company
            analytics.PoliciesByCompany = await _context.InsuranceCompanies
                .Select(c => new CompanyAnalyticsDto
                {
                    CompanyName = c.CompanyName,
                    TotalPolicies = c.Policies.Count
                })
                .OrderByDescending(c => c.TotalPolicies)
                .ToListAsync();

            // Top Agents
            analytics.TopAgents = await _context.Agents
                .Select(a => new AgentAnalyticsDto
                {
                   AgentId = a.AgentId,
                   AgentName = a.FirstName + " " + a.LastName,
                   TotalPolicies = a.Policies.Count()
                })
                   .OrderByDescending(a => a.TotalPolicies)
                   .Take(5)
                   .ToListAsync();

            // Monthly Premium Collection
            analytics.MonthlyPremiumCollection = await _context.PremiumPayments
                .GroupBy(p => new { p.CreatedDate.Year, p.CreatedDate.Month })
                .Select(g => new MonthlyPremiumDto
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalPremium = g.Sum(x => x.Amount)
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToListAsync();

            return analytics;
        }



    }
}