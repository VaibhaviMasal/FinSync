using FinSync.Application.Features.InsurancePlans.DTOs;
using FluentValidation;

namespace FinSync.Application.Features.InsurancePlans.Validators
{
    public class UpdateInsurancePlanRequestDtoValidator
        : AbstractValidator<UpdateInsurancePlanRequestDto>
    {
        public UpdateInsurancePlanRequestDtoValidator()
        {
            RuleFor(x => x.CompanyId)
                .GreaterThan(0);

            RuleFor(x => x.PlanName)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.PlanCode)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.PlanType)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Description)
                .MaximumLength(500);

            RuleFor(x => x.MinimumAge)
                .InclusiveBetween(0, 100);

            RuleFor(x => x.MaximumAge)
                .InclusiveBetween(0, 100);

            RuleFor(x => x.PolicyTerm)
                .GreaterThan(0);

            RuleFor(x => x.PremiumFrequency)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.MinimumSumAssured)
                .GreaterThan(0);

            RuleFor(x => x.MaximumSumAssured)
                .GreaterThan(0);

            RuleFor(x => x.IsActive)
                .NotNull();
        }
    }
}