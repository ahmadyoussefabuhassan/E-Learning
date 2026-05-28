using FluentValidation;

namespace E_Learning.Application.Teachers.RegisterTeacher
{
    internal sealed class RegisterTeacherCommandValidator : AbstractValidator<RegisterTeacherCommand>
    {
        public RegisterTeacherCommandValidator()
        {
            RuleFor(Teachers => Teachers.FullName)
                .NotEmpty().WithMessage("الاسم الكامل مطلوب")
                .MaximumLength(100).WithMessage("الاسم الكامل لا يجب أن يتجاوز 100 حرف");
            RuleFor(Teachers => Teachers.Email)
                .NotEmpty().WithMessage("البريد الإلكتروني مطلوب")
                .EmailAddress().WithMessage("البريد الإلكتروني غير صالح");
            RuleFor(Teachers => Teachers.Password)
                .NotEmpty().WithMessage("كلمة المرور مطلوبة")
                .MinimumLength(6).WithMessage("كلمة المرور يجب أن تكون على الأقل 6 أحرف");
            RuleFor(Teachers => Teachers.PhoneNumber)
                .NotEmpty().WithMessage("رقم الهاتف مطلوب")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("رقم الهاتف غير صالح");
            RuleFor(Teachers => Teachers.Address)
                .NotEmpty().WithMessage("العنوان مطلوب")
                .MaximumLength(200).WithMessage("العنوان لا يجب أن يتجاوز 200 حرف");
            RuleFor(Teachers => Teachers.Education)
                .NotEmpty().WithMessage("المؤهل العلمي مطلوب");
            RuleFor(Teachers => Teachers.SahmCash)
                .NotEmpty().WithMessage("سهم الكاش مطلوب");
        }
    }
}
