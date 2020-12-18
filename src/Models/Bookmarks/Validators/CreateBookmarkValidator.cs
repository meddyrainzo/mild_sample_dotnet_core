using FluentValidation;
using Models.Exceptions;

namespace Models.Bookmarks
{
    public class CreateBookmarkValidator : AbstractValidator<CreateBookmarkRequest>
    {
        public CreateBookmarkValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage(ErrorReason.INVALID_ID);
            RuleFor(x => x.QuoteId).NotEmpty().WithMessage(ErrorReason.INVALID_ID);
        }
    }
}