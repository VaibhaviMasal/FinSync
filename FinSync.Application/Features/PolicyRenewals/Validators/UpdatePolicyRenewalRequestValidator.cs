using FinSync.Application.Features.PolicyRenewals.DTOs;
using FluentValidation;

namespace FinSync.Application.Features.PolicyRenewals.Validators
{
    public class UpdatePolicyRenewalRequestValidator
        : AbstractValidator<UpdatePolicyRenewalRequestDto>
    {
        public UpdatePolicyRenewalRequestValidator()
        {
            RuleFor(x => x.PolicyId)
                .GreaterThan(0)
                .WithMessage("Policy ID must be greater than zero.");

            RuleFor(x => x.RenewalDate)
                .NotEmpty()
                .WithMessage("Renewal Date is required.");

            RuleFor(x => x.NewExpiryDate)
                .NotEmpty()
                .WithMessage("New Expiry Date is required.")
                .Must((dto, newExpiryDate) => newExpiryDate > dto.RenewalDate)
                .WithMessage("New Expiry Date must be later than Renewal Date.");

            RuleFor(x => x.RenewalPremium)
                .GreaterThan(0)
                .WithMessage("Renewal Premium must be greater than zero.");

            RuleFor(x => x.Remarks)
                .MaximumLength(500)
                .WithMessage("Remarks cannot exceed 500 characters.");
        }
    }
}