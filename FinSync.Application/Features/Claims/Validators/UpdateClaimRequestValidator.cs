using FinSync.Application.Features.Claims.DTOs;
using FluentValidation;

namespace FinSync.Application.Features.Claims.Validators
{
    public class UpdateClaimRequestValidator
        : AbstractValidator<UpdateClaimRequestDto>
    {
        public UpdateClaimRequestValidator()
        {
            RuleFor(x => x.ClaimAmount)
                .GreaterThan(0);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x)
                .Must(x => x.IncidentDate <= x.ClaimDate)
                .WithMessage("Incident Date cannot be later than Claim Date.");

            RuleFor(x => x)
                .Must(x =>
                    !x.SettlementDate.HasValue ||
                    x.SettlementDate.Value >= x.ClaimDate)
                .WithMessage("Settlement Date must be after Claim Date.");
        }
    }
}