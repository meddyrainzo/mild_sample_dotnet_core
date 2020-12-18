using FluentValidation;
using Models.Exceptions;

namespace Models.Users
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(ErrorReason.INVALID_ID);
            RuleFor(x => x.UserName).NotEmpty().WithMessage(ErrorReason.EMPTY_USERNAME);
        }

    }

}