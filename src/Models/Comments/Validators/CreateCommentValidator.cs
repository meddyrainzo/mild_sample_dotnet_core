using FluentValidation;
using Models.Exceptions;

namespace Models.Comments
{
    public class CreateCommentValidator : AbstractValidator<CreateCommentRequest>
    {
        public CreateCommentValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage(ErrorReason.INVALID_ID);
            RuleFor(x => x.QuoteId).NotEmpty().WithMessage(ErrorReason.INVALID_ID);
            RuleFor(x => x.Comment).NotEmpty().WithMessage(ErrorReason.EMPTY_COMMENT);
        }
    }
}