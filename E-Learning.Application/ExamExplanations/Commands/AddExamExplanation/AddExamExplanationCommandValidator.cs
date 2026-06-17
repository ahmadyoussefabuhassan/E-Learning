using FluentValidation;

namespace E_Learning.Application.ExamExplanations.Commands.AddExamExplanation
{
    internal sealed class AddExamExplanationCommandValidato : AbstractValidator<AddExamExplanationCommand>
    {
        public AddExamExplanationCommandValidato()
        {
            RuleFor(command => command.Title)
                .NotEmpty().WithMessage("اسم اسئلة الدورات مطلوب")
                .MaximumLength(100).WithMessage("اسم اسئلة الدورات لا يجب أن يتجاوز 100 حرف");
            RuleFor(command => command.Description)
                .NotEmpty().WithMessage("وصف اسئلة الدورات مطلوب")
                .MaximumLength(1000).WithMessage("وصف اسئلة الدورات لا يجب أن يتجاوز 1000 حرف");
            RuleFor(command => command.Price)
                .GreaterThanOrEqualTo(0).WithMessage("السعر يجب أن يكون أكبر من أو يساوي 0");
        }
    }
}
