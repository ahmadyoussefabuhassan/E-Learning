using FluentValidation;

namespace E_Learning.Application.Users.Commands.ChangePassword
{
    internal sealed class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(command => command.OldPassword)
                .NotEmpty().WithMessage("كلمة المرور قديمة مطلوبة")
                .MinimumLength(6).WithMessage("كلمة المرور يجب أن تكون على الأقل 6 أحرف"); ;
            RuleFor(command => command.NewPassword)
                 .NotEmpty().WithMessage("كلمة المرور مطلوبة")
                 .MinimumLength(6).WithMessage("كلمة المرور يجب أن تكون على الأقل 6 أحرف");
            RuleFor(command => command.ChekPassword)
                .NotEmpty().WithMessage("يجب مطابقة كلمة المرور الجديدة")
                .MinimumLength(6).WithMessage("كلمة المرور يجب أن تكون على الأقل 6 أحرف");
        }
    }
}
