using FluentValidation;

namespace E_Learning.Application.Users.Commands.ForgotPassword
{
    internal sealed class SendResetCodeCommandValidator : AbstractValidator<SendResetCodeCommand>
    {
        public SendResetCodeCommandValidator()
        {
            RuleFor(command => command.Email)
               .NotEmpty().WithMessage("البريد الإلكتروني مطلوب")
               .EmailAddress().WithMessage("البريد الإلكتروني غير صالح");
        }
    }
}
