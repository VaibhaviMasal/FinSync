using FinSync.Domain.Enums;

namespace FinSync.Application.Features.PolicyRenewals.DTOs
{
    public class PolicyRenewalResponseDto
    {
        public int RenewalId { get; set; }

        public int PolicyId { get; set; }

        public string PolicyNumber { get; set; } = string.Empty;

        public DateOnly RenewalDate { get; set; }

        public DateOnly OldExpiryDate { get; set; }

        public DateOnly NewExpiryDate { get; set; }

        public decimal RenewalPremium { get; set; }

        public RenewalStatus RenewalStatus { get; set; }

        public string? Remarks { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}