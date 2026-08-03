namespace FinSync.Application.Features.Dashboard.DTOs
{
    public class AgentAnalyticsDto
    {
        public int AgentId { get; set; }

        public string AgentName { get; set; } = string.Empty;

        public int TotalPolicies { get; set; }
    }
}