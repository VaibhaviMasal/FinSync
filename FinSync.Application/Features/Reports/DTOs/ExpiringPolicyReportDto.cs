namespace FinSync.Application.Features.Reports.DTOs
{
    public class ExpiringPolicyReportDto
    {
        public string PolicyNumber { get; set; } = string.Empty;

        public string CustomerName { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

        public DateTime ExpiryDate { get; set; }

        public int DaysRemaining { get; set; }

        public decimal PremiumAmount { get; set; }

        public string AgentName { get; set; } = string.Empty;
    }
}