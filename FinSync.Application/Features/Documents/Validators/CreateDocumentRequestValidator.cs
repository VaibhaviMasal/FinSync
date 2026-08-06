using FinSync.Application.Features.Documents.DTOs;
using FluentValidation;

namespace FinSync.Application.Features.Documents.Validators
{
    public class CreateDocumentRequestValidator
        : AbstractValidator<CreateDocumentRequestDto>
    {
        public CreateDocumentRequestValidator()
        {
            RuleFor(x => x.PolicyId)
                .GreaterThan(0);

            RuleFor(x => x.DocumentType)
                .NotEmpty();

            RuleFor(x => x.OriginalFileName)
                .NotEmpty();

            RuleFor(x => x.ContentType)
                .NotEmpty();

            RuleFor(x => x.FileBytes)
                .NotEmpty()
                .Must(x => x.Length <= 10 * 1024 * 1024)
                .WithMessage("Maximum file size is 10 MB.");
        }
    }
}