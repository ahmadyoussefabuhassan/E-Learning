using FluentValidation;

namespace E_Learning.Application.StudentSubscriptions.Commands.RegisterSection
{
    internal sealed class RegisterSectionCommandValidator : AbstractValidator<RegisterSectionCommand>
    {
        public RegisterSectionCommandValidator() 
        {
            RuleFor(command => command.targetId)
                 .NotNull();
            RuleFor(command => command.ReceiptImageUrl)
                .NotNull().WithMessage("صورة الأشعار مطلوبة")
                .Must(file => file.ContentType.StartsWith("image/")).WithMessage("الملف يجب أن يكون صورة");
        }
    }
}
