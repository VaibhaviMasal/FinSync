namespace FinSync.Application.Features.Reports.DTOs
{
    public class AgentPerformanceReportDto
    {
        public int AgentId { get; set; }

        public string AgentName { get; set; } = string.Empty;

        public int PoliciesSold { get; set; }

        public decimal TotalBusiness { get; set; }

        public int ActivePolicies { get; set; }

        public int Renewals { get; set; }

        public int ClaimsHandled { get; set; }
    }
}