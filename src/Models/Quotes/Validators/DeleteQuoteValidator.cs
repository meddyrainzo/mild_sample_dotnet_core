using FluentValidation;
using Models.Exceptions;

namespace Models.Quotes
{
    public class DeleteQuoteValidator : AbstractValidator<DeleteQuoteRequest>
    {
        public DeleteQuoteValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(ErrorReason.INVALID_ID);
            RuleFor(x => x.UserId).NotEmpty().WithMessage(ErrorReason.INVALID_ID);
        }
    }

}