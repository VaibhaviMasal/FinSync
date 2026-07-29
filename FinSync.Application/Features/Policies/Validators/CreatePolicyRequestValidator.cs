using FinSync.Application.Features.Policies.DTOs;
using FluentValidation;

namespace FinSync.Application.Features.Policies.Validators;

public class CreatePolicyRequestValidator : AbstractValidator<CreatePolicyRequestDto>
{
    public CreatePolicyRequestValidator()
    {
        RuleFor(x => x.PolicyNumber)
            .NotEmpty().WithMessage("Policy Number is required.");

        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("Valid Customer is required.");

        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("Valid Insurance Company is required.");

        RuleFor(x => x.PlanId)
            .GreaterThan(0).WithMessage("Valid Insurance Plan is required.");

        RuleFor(x => x.PremiumAmount)
            .GreaterThan(0).WithMessage("Premium Amount must be greater than zero.");

        RuleFor(x => x.SumAssured)
            .GreaterThan(0).WithMessage("Sum Assured must be greater than zero.");

        RuleFor(x => x.IssueDate)
            .NotEmpty().WithMessage("Issue Date is required.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start Date is required.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("End Date is required.");

        RuleFor(x => x)
            .Must(x => x.StartDate <= x.EndDate)
            .WithMessage("Start Date cannot be later than End Date.");

        RuleFor(x => x.PremiumFrequency)
    .IsInEnum()
    .WithMessage("Valid Premium Frequency is required.");



        RuleFor(x => x.NomineeName)
            .NotEmpty().WithMessage("Nominee Name is required.");

        RuleFor(x => x.NomineeRelation)
            .NotEmpty().WithMessage("Nominee Relation is required.");

        RuleFor(x => x.NomineePhoneNumber)
            .NotEmpty().WithMessage("Nominee Phone Number is required.")
            .Matches(@"^[0-9]{10}$")
            .WithMessage("Nominee Phone Number must be exactly 10 digits.");
    }
}