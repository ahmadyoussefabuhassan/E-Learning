using FluentValidation;

namespace E_Learning.Application.Classes.Commands.UpdateClass
{
    internal sealed class UpdateClassCommandValidtor : AbstractValidator<UpdateClassCommand>
    {
        public UpdateClassCommandValidtor()
        {
            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("اسم الصف مطلوب")
                .MaximumLength(100).WithMessage("اسم الصف لا يجب أن يتجاوز 100 حرف");
        }
    }
}
