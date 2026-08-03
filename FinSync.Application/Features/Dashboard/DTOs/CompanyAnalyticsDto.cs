namespace FinSync.Application.Features.Dashboard.DTOs
{
    public class CompanyAnalyticsDto
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public int TotalPolicies { get; set; }
    }
}