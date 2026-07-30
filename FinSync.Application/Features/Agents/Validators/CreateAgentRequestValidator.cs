using FinSync.Application.Features.Agents.DTOs;
using FluentValidation;

namespace FinSync.Application.Features.Agents.Validators
{
    public class CreateAgentRequestValidator : AbstractValidator<CreateAgentRequestDto>
    {
        public CreateAgentRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.MobileNumber)
                .NotEmpty()
                .Matches(@"^[6-9]\d{9}$")
                .WithMessage("Enter a valid mobile number.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.PanNumber)
                .NotEmpty()
                .Matches(@"^[A-Z]{5}[0-9]{4}[A-Z]{1}$");

            RuleFor(x => x.AadhaarNumber)
                .NotEmpty()
                .Length(12);

            RuleFor(x => x.LicenseNumber)
                .NotEmpty();

            RuleFor(x => x.Address)
                .NotEmpty();

            RuleFor(x => x.City)
                .NotEmpty();

            RuleFor(x => x.State)
                .NotEmpty();

            RuleFor(x => x.Pincode)
                .NotEmpty()
                .Length(6);
        }
    }
}