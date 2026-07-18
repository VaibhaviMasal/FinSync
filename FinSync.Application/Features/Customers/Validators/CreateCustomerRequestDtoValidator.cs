using FinSync.Application.Features.Customers.DTOs;
using FluentValidation;

namespace FinSync.Application.Features.Customers.Validators
{
    public class CreateCustomerRequestDtoValidator : AbstractValidator<CreateCustomerRequestDto>
    {
        public CreateCustomerRequestDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .MaximumLength(50);

            RuleFor(x => x.MobileNumber)
                .NotEmpty().WithMessage("Mobile Number is required.")
                .Matches(@"^[6-9]\d{9}$")
                .WithMessage("Enter a valid 10-digit mobile number.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Enter a valid email address.");

            RuleFor(x => x.PanNumber)
                .NotEmpty()
                .Matches(@"^[A-Z]{5}[0-9]{4}[A-Z]$")
                .WithMessage("Invalid PAN number.");

            RuleFor(x => x.AadhaarNumber)
                .NotEmpty()
                .Matches(@"^\d{12}$")
                .WithMessage("Aadhaar number must contain exactly 12 digits.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.");

            RuleFor(x => x.State)
                .NotEmpty().WithMessage("State is required.");

            RuleFor(x => x.Pincode)
                .NotEmpty()
                .Matches(@"^\d{6}$")
                .WithMessage("Pincode must be exactly 6 digits.");

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("Date of Birth must be in the past.");

            RuleFor(x => x.AnnualIncome)
                .GreaterThanOrEqualTo(0)
                .When(x => x.AnnualIncome.HasValue)
                .WithMessage("Annual Income cannot be negative.");
        }
    }
}