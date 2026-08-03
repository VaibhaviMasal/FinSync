namespace FinSync.Application.Features.Dashboard.DTOs
{
    public class DashboardSummaryDto
    {
        public int TotalCustomers { get; set; }

        public int TotalAgents { get; set; }

        public int TotalInsuranceCompanies { get; set; }

        public int TotalPlans { get; set; }

        public int TotalPolicies { get; set; }

        public int ActivePolicies { get; set; }

        public int ExpiredPolicies { get; set; }

        public int PoliciesExpiringSoon { get; set; }

        public decimal TotalPremiumCollected { get; set; }

        public int PremiumsPaid { get; set; }

        public int PremiumsPending { get; set; }
    }
}