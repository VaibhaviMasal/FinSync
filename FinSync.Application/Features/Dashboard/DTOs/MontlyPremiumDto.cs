namespace FinSync.Application.Features.Dashboard.DTOs
{
    public class MonthlyPremiumDto
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public decimal TotalPremium { get; set; }
    }
}