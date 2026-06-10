using FluentValidation;

namespace E_Learning.Application.Students.Commands.UpdateProfileStudent
{
    internal sealed class UpdateProfileStudentCommandValidator : AbstractValidator<UpdateProfileStudentCommand>
    {
        public UpdateProfileStudentCommandValidator()
        {
            RuleFor(command => command.FullName)
             .NotEmpty().WithMessage("الاسم الكامل مطلوب")
             .MaximumLength(100).WithMessage("الاسم الكامل لا يجب أن يتجاوز 100 حرف");
            RuleFor(command => command.Email)
                .NotEmpty().WithMessage("البريد الإلكتروني مطلوب")
                .EmailAddress().WithMessage("البريد الإلكتروني غير صالح");
            RuleFor(command => command.PhoneNumber)
                .NotEmpty().WithMessage("رقم الهاتف مطلوب")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("رقم الهاتف غير صالح");
            RuleFor(command => command.Address)
                .NotEmpty().WithMessage("العنوان مطلوب")
                .MaximumLength(200).WithMessage("العنوان لا يجب أن يتجاوز 200 حرف");
            RuleFor(command => command.Education)
                .NotEmpty().WithMessage("المؤهل العلمي مطلوب");
        }
    }
}
