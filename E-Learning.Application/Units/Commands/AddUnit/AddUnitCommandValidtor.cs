using FluentValidation;


namespace E_Learning.Application.Units.Commands.AddUnit
{
    internal sealed class AddUnitCommandValidtor : AbstractValidator<AddUnitCommand>
    {
        public AddUnitCommandValidtor()
        {
            RuleFor(command => command.Title)
              .NotEmpty().WithMessage("اسم الوحدة مطلوب")
              .MaximumLength(100).WithMessage("اسم الوحدة لا يجب أن يتجاوز 100 حرف");
            RuleFor(command => command.Description)
                .NotEmpty().WithMessage("وصف الوحدة مطلوب")
                .MaximumLength(1000).WithMessage("وصف الوحدة لا يجب أن يتجاوز 1000 حرف");
        }
    }
}
