using FluentValidation;

namespace E_Learning.Application.Teachers.Commands.LoginTeacher
{
    internal sealed class LogInTeacherCommandValidator : AbstractValidator<LogInTeacherCommand>
    {
        public LogInTeacherCommandValidator()
        {
            RuleFor(command => command.Email)
                .NotEmpty().WithMessage("البريد الإلكتروني مطلوب")
                .EmailAddress().WithMessage("البريد الإلكتروني غير صالح");
            RuleFor(command => command.Password)
                .NotEmpty().WithMessage("كلمة المرور مطلوبة")
                .MinimumLength(6).WithMessage("كلمة المرور يجب أن تكون على الأقل 6 أحرف");
        }
    }
}
