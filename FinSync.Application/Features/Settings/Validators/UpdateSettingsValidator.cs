using FinSync.Application.Features.Settings.DTOs;
using FluentValidation;

namespace FinSync.Application.Features.Settings.Validators
{
    public class UpdateSettingsRequestValidator
        : AbstractValidator<UpdateSettingsRequestDto>
    {
        public UpdateSettingsRequestValidator()
        {
            RuleFor(x => x.BusinessName).NotEmpty();
            RuleFor(x => x.OwnerName).NotEmpty();

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches(@"^[6-9]\d{9}$");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Address).NotEmpty();

            RuleFor(x => x.Currency).NotEmpty();
        }
    }
}