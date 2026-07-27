using FinSync.Domain.Enums;

namespace FinSync.Application.Features.PolicyRenewals.DTOs
{
    public class UpdatePolicyRenewalRequestDto
    {
        public int PolicyId { get; set; }

        public DateOnly RenewalDate { get; set; }

        public DateOnly OldExpiryDate { get; set; }

        public DateOnly NewExpiryDate { get; set; }

        public decimal RenewalPremium { get; set; }

        public RenewalStatus RenewalStatus { get; set; }

        public string? Remarks { get; set; }
    }
}