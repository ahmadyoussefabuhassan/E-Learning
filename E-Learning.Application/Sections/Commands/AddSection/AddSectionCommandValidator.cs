using FluentValidation;

namespace E_Learning.Application.Sections.Commands.AddSection
{
    internal sealed class AddSectionCommandValidator : AbstractValidator<AddSectionCommand>
    {
        public AddSectionCommandValidator() 
        {
            RuleFor(command => command.Title)
                .NotEmpty().WithMessage("اسم قسم مطلوب")
                .MaximumLength(100).WithMessage("اسم قسم لا يجب أن يتجاوز 100 حرف");
            RuleFor(command => command.Price)
                .GreaterThanOrEqualTo(0).WithMessage("السعر يجب أن يكون أكبر من أو يساوي 0");
        }
    }
}
