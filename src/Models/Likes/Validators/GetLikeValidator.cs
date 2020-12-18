using FluentValidation;
using Models.Exceptions;

namespace Models.Likes
{
    public class GetLikeValidator : AbstractValidator<GetLikeRequest>
    {
        public GetLikeValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage(ErrorReason.INVALID_ID);
            RuleFor(x => x.QuoteId).NotEmpty().WithMessage(ErrorReason.INVALID_ID);
        }
    }
}