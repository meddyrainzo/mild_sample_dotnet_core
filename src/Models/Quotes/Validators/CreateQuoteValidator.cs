using FluentValidation;
using Models.Exceptions;

namespace Models.Quotes
{
    public class CreateQuoteValidator : AbstractValidator<CreateQuoteRequest>
    {
        public CreateQuoteValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage(ErrorReason.INVALID_ID);
            RuleFor(x => x.Quote).NotEmpty().WithMessage(ErrorReason.EMPTY_QUOTE);
        }
    }

}