using FluentValidation;
using Models.Exceptions;

namespace Models.Quotes
{
    public class UpdateQuoteValidator : AbstractValidator<UpdateQuoteRequest>
    {
        public UpdateQuoteValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(ErrorReason.INVALID_ID);
            RuleFor(x => x.UserId).NotEmpty().WithMessage(ErrorReason.INVALID_ID);
            RuleFor(x => x.Quote).NotEmpty().WithMessage(ErrorReason.EMPTY_QUOTE);
        }
    }

}