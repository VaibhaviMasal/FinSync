namespace FinSync.Application.Features.Reports.DTOs
{
    public class PremiumCollectionReportDto
    {
        public decimal TotalPremiumCollected { get; set; }

        public decimal PendingPremium { get; set; }

        public decimal PaidPremium { get; set; }

        public int TotalPayments { get; set; }

        public int PaidPayments { get; set; }

        public int PendingPayments { get; set; }
    }
}