using FluentValidation;

namespace E_Learning.Application.Classes.Commands.UpdateClass
{
    internal sealed class UpdateClassCommandValidator : AbstractValidator<UpdateClassCommand>
    {
        public UpdateClassCommandValidator()
        {
            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("اسم الصف مطلوب")
                .MaximumLength(100).WithMessage("اسم الصف لا يجب أن يتجاوز 100 حرف");
        }
    }
}
