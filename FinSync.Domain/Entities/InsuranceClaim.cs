using FinSync.Domain.Enums;

namespace FinSync.Domain.Entities
{
    public class InsuranceClaim
    {
        public int ClaimId { get; set; }

        public string ClaimNumber { get; set; } = string.Empty;

        public int PolicyId { get; set; }

        public ClaimType ClaimType { get; set; }

        public decimal ClaimAmount { get; set; }

        public decimal? ApprovedAmount { get; set; }

        public DateTime IncidentDate { get; set; }

        public DateTime ClaimDate { get; set; }

        public DateTime? SettlementDate { get; set; }

        public ClaimStatus ClaimStatus { get; set; } = ClaimStatus.Pending;

        public string Description { get; set; } = string.Empty;

        public string? Remarks { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        // Navigation Property
        public Policy Policy { get; set; } = null!;
    }
}