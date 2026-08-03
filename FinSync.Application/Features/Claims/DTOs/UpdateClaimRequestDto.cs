using FinSync.Domain.Enums;

namespace FinSync.Application.Features.Claims.DTOs
{
    public class UpdateClaimRequestDto
    {
        public ClaimType ClaimType { get; set; }

        public decimal ClaimAmount { get; set; }

        public decimal? ApprovedAmount { get; set; }

        public DateOnly IncidentDate { get; set; }

        public DateOnly ClaimDate { get; set; }

        public DateOnly? SettlementDate { get; set; }

        public ClaimStatus ClaimStatus { get; set; }

        public string Description { get; set; } = string.Empty;

        public string? Remarks { get; set; }
    }
}