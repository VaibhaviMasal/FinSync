using FinSync.Domain.Enums;

namespace FinSync.Application.Features.Reports.DTOs
{
    public class ClaimReportDto
    {
        public string ClaimNumber { get; set; } = string.Empty;

        public string PolicyNumber { get; set; } = string.Empty;

        public string CustomerName { get; set; } = string.Empty;

        public decimal ClaimAmount { get; set; }

        public decimal? ApprovedAmount { get; set; }

        public ClaimStatus ClaimStatus { get; set; }

        public DateTime ClaimDate { get; set; }

        public DateTime? SettlementDate { get; set; }
    }
}