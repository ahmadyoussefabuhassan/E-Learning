using FluentValidation;

namespace E_Learning.Application.Users.Commands.ChangePasswordResetCode
{
    internal sealed class ChangePasswordResetCodeCommandValidator : AbstractValidator<ChangePasswordResetCodeCommand>
    {
        public ChangePasswordResetCodeCommandValidator()
        {
            RuleFor(command => command.Password)
                .NotEmpty().WithMessage("كلمة المرور مطلوبة")
                .MinimumLength(6).WithMessage("كلمة المرور يجب أن تكون على الأقل 6 أحرف");
        }
    }
}
