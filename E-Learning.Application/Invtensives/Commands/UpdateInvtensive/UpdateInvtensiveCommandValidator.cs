using FluentValidation;

namespace E_Learning.Application.Invtensives.Commands.UpdateInvtensive
{
    internal sealed class UpdateInvtensiveCommandValidator : AbstractValidator<UpdateInvtensiveCommand>
    {
        public UpdateInvtensiveCommandValidator()
        {
            RuleFor(command => command.Title)
                .NotEmpty().WithMessage("اسم المكثفة مطلوب")
                .MaximumLength(100).WithMessage("اسم المكثفة لا يجب أن يتجاوز 100 حرف");
            RuleFor(command => command.Description)
                .NotEmpty().WithMessage("وصف المكثفة مطلوب")
                .MaximumLength(1000).WithMessage("وصف المكثفة لا يجب أن يتجاوز 1000 حرف");
            RuleFor(command => command.Price)
                .GreaterThanOrEqualTo(0).WithMessage("السعر يجب أن يكون أكبر من أو يساوي 0");
        }
    }
}
