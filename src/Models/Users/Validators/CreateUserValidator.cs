using FluentValidation;
using Models.Exceptions;

namespace Models.Users
{
    public class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage(ErrorReason.EMPTY_USERNAME);
        }

    }

}