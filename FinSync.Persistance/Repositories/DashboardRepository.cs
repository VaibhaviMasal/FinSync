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
            throw new NotImplementedException();
        }

        public async Task<DashboardAnalyticsDto> GetDashboardAnalyticsAsync()
        {
            throw new NotImplementedException();
        }



    }
}