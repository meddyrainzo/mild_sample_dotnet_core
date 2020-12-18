using FluentValidation;
using Models.Exceptions;

namespace Models.Likes
{
    public class CreateLikeValidator : AbstractValidator<CreateLikeRequest>
    {
        public CreateLikeValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage(ErrorReason.INVALID_ID);
            RuleFor(x => x.QuoteId).NotEmpty().WithMessage(ErrorReason.INVALID_ID);
        }
    }
}