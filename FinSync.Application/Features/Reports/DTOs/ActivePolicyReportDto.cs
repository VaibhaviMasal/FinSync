namespace FinSync.Application.Features.Reports.DTOs
{
    public class ActivePolicyReportDto
    {
        public string PolicyNumber { get; set; } = string.Empty;

        public string CustomerName { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

        public string PlanName { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal PremiumAmount { get; set; }

        public string AgentName { get; set; } = string.Empty;
    }
}