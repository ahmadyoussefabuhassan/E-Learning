using FluentValidation;

namespace E_Learning.Application.Classes.Commands.AddClass
{
    internal sealed class AddClassCommandValidator : AbstractValidator<AddClassCommand>
    {
        public AddClassCommandValidator()
        {
            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("اسم الصف مطلوب")
                .MaximumLength(100).WithMessage("اسم الصف لا يجب أن يتجاوز 100 حرف");
        }
    }
}
