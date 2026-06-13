using FluentValidation;

namespace E_Learning.Application.Users.Commands.VerifyResetCode
{
    internal sealed class VerifyResetCodeCommandValidator : AbstractValidator<VerifyResetCodeCommand>
    {
        public VerifyResetCodeCommandValidator()
        {
            RuleFor(command => command.Code)
                .NotEmpty().WithMessage("الرمز تحقق مطلوب")
                .MinimumLength(6).WithMessage(" الرمز تحقق غير صالح");
        }
    }
}
