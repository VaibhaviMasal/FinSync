namespace FinSync.Application.Features.Reports.DTOs
{
    public class DashboardReportDto
    {
        public int TotalCustomers { get; set; }

        public int TotalAgents { get; set; }

        public int TotalPolicies { get; set; }

        public int ActivePolicies { get; set; }

        public int ExpiringPolicies { get; set; }

        public int PendingClaims { get; set; }

        public decimal PremiumCollected { get; set; }

        public decimal TotalBusiness { get; set; }
    }
}