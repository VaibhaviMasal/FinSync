namespace FinSync.Application.Features.Reports.DTOs
{
    public class CompanyBusinessReportDto
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public int TotalPolicies { get; set; }

        public int TotalCustomers { get; set; }

        public decimal TotalPremium { get; set; }

        public int TotalClaims { get; set; }
    }
}