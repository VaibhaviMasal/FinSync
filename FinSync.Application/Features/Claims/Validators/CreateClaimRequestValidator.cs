using FinSync.Application.Features.Claims.DTOs;
using FluentValidation;

namespace FinSync.Application.Features.Claims.Validators
{
    public class CreateClaimRequestValidator
        : AbstractValidator<CreateClaimRequestDto>
    {
        public CreateClaimRequestValidator()
        {
            RuleFor(x => x.PolicyId)
                .GreaterThan(0)
                .WithMessage("Valid Policy is required.");

            RuleFor(x => x.ClaimAmount)
                .GreaterThan(0)
                .WithMessage("Claim Amount must be greater than zero.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.IncidentDate)
                .NotEmpty();

            RuleFor(x => x.ClaimDate)
                .NotEmpty();

            RuleFor(x => x)
                .Must(x => x.IncidentDate <= x.ClaimDate)
                .WithMessage("Incident Date cannot be later than Claim Date.");
        }
    }
}