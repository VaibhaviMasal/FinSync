namespace FinSync.Application.Features.Dashboard.DTOs
{
    public class DashboardAlertDto
    {
        public int PolicyId { get; set; }

        public string PolicyNumber { get; set; } = string.Empty;

        public string CustomerName { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

        public DateTime ExpiryDate { get; set; }

        public int DaysRemaining { get; set; }
    }
}