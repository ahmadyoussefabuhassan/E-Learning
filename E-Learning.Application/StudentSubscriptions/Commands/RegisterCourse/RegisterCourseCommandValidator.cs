using FluentValidation;

namespace E_Learning.Application.StudentSubscriptions.Commands.RegisterCourse
{
    internal sealed class RegisterCourseCommandValidator : AbstractValidator<RegisterCourseCommand>
    {
        public RegisterCourseCommandValidator()
        {
            RuleFor(command => command.targetId)
                .NotNull();
            RuleFor(command => command.ReceiptImageUrl)
                .NotNull().WithMessage("صورة الأشعار مطلوبة")
                .Must(file => file.ContentType.StartsWith("image/")).WithMessage("الملف يجب أن يكون صورة");
        }
    }
}
