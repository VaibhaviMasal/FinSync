namespace FinSync.Application.Features.Dashboard.DTOs
{
    public class DashboardAnalyticsDto
    {
        public List<CompanyAnalyticsDto> PoliciesByCompany { get; set; } = new();

        public List<AgentAnalyticsDto> TopAgents { get; set; } = new();

        public List<MonthlyPremiumDto> MonthlyPremiumCollection { get; set; } = new();
    }
}