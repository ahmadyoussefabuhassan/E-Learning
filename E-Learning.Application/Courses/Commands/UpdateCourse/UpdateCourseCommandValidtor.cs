using FluentValidation;

namespace E_Learning.Application.Courses.Commands.UpdateCourse
{
    internal sealed class UpdateCourseCommandValidtor : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidtor()
        {
            RuleFor(command => command.Title)
                .NotEmpty().WithMessage("اسم الدورة مطلوب")
                .MaximumLength(100).WithMessage("اسم الدورة لا يجب أن يتجاوز 100 حرف");
            RuleFor(command => command.Description)
                .NotEmpty().WithMessage("وصف الدورة مطلوب")
                .MaximumLength(1000).WithMessage("وصف الدورة لا يجب أن يتجاوز 1000 حرف");
            RuleFor(command => command.Price)
                .GreaterThanOrEqualTo(0).WithMessage("السعر يجب أن يكون أكبر من أو يساوي 0");
            RuleFor(command => command.ImageFile)
                .NotNull().WithMessage("صورة الدورة مطلوبة")
                .Must(file => file.ContentType.StartsWith("image/")).WithMessage("الملف يجب أن يكون صورة");
            RuleFor(command => command.ClassroomName)
                .NotNull().WithMessage("اسم الصف مطلوب");
        }
    }
}
