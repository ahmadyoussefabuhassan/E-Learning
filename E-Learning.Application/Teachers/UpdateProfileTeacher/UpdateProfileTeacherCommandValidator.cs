using FluentValidation;


namespace E_Learning.Application.Teachers.UpdateProfileTeacher
{
    internal sealed class UpdateProfileTeacherCommandValidator : AbstractValidator<UpdateProfileTeacherCommand>
    {
        public UpdateProfileTeacherCommandValidator()
        {
            RuleFor(command => command.FullName)
                .NotEmpty().WithMessage("الاسم الكامل مطلوب")
                .MaximumLength(100).WithMessage("الاسم الكامل لا يجب أن يتجاوز 100 حرف");
            RuleFor(command => command.Email)
                .NotEmpty().WithMessage("البريد الإلكتروني مطلوب")
                .EmailAddress().WithMessage("البريد الإلكتروني غير صالح");
            RuleFor(command => command.PhoneNumber)
                .NotEmpty().WithMessage("رقم الهاتف مطلوب")
                .Matches(@"^\+?\d{10,15}$").WithMessage("رقم الهاتف غير صالح");
            RuleFor(command => command.Address)
                .NotEmpty().WithMessage("العنوان مطلوب")
                .MaximumLength(200).WithMessage("العنوان لا يجب أن يتجاوز 200 حرف");
            RuleFor(command => command.SahmCash)
                .NotEmpty().WithMessage("شام كاش مطلوب")
                .Matches(@"^\d+(\.\d{1,2})?$").WithMessage("شام كاش يجب أن يكون رقماً صالحاً");
            RuleFor(command => command.Education)
                .NotEmpty().WithMessage("المؤهل العلمي مطلوب");

        }
    }
}
