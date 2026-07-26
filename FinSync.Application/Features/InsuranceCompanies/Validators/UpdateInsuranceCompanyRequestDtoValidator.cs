using FinSync.Application.Features.InsuranceCompanies.DTOs;
using FluentValidation;

namespace FinSync.Application.Features.InsuranceCompanies.Validators
{
    public class UpdateInsuranceCompanyRequestDtoValidator
        : AbstractValidator<UpdateInsuranceCompanyRequestDto>
    {
        public UpdateInsuranceCompanyRequestDtoValidator()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.CompanyCode)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.ContactPerson)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches(@"^[6-9]\d{9}$")
                .WithMessage("Phone Number must be a valid 10-digit Indian mobile number.");

            RuleFor(x => x.Website)
                .NotEmpty()
                .Must(url =>
                    Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("Website must be a valid URL.");

            RuleFor(x => x.AddressLine1)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.City)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.State)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Pincode)
                .NotEmpty()
                .Matches(@"^\d{6}$")
                .WithMessage("Pincode must be exactly 6 digits.");
        }
    }
}