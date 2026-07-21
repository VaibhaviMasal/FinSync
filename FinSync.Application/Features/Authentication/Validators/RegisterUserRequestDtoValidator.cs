using FinSync.Application.Features.Authentication.DTOs;
using FluentValidation;

namespace FinSync.Application.Features.Authentication.Validators
{
    public class RegisterUserRequestDtoValidator : AbstractValidator<RegisterUserRequestDto>
    {
        public RegisterUserRequestDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Full Name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Please enter a valid email address.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters long.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Confirm Password is required.")
                .Equal(x => x.Password)
                .WithMessage("Passwords do not match.");

            RuleFor(x => x.Role)
                .NotEmpty()
                .WithMessage("Role is required.");
        }
    }
}