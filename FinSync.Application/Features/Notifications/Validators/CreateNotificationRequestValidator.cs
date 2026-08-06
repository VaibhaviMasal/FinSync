using FinSync.Application.Features.Notifications.DTOs;
using FluentValidation;

namespace FinSync.Application.Features.Notifications.Validators
{
    public class CreateNotificationRequestValidator
        : AbstractValidator<CreateNotificationRequestDto>
    {
        public CreateNotificationRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Message)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.NotificationType)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}