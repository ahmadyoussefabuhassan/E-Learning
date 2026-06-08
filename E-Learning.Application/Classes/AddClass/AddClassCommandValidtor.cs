using FluentValidation;

namespace E_Learning.Application.Classes.AddClass
{
    internal sealed class AddClassCommandValidtor : AbstractValidator<AddClassCommand>
    {
        public AddClassCommandValidtor()
        {
            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("اسم الصف مطلوب")
                .MaximumLength(100).WithMessage("اسم الصف لا يجب أن يتجاوز 100 حرف");
        }
    }
}
