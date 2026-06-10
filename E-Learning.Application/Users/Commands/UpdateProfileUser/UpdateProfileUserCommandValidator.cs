using FluentValidation;


namespace E_Learning.Application.Users.Commands.UpdateProfileUser
{
    internal sealed class UpdateProfileUserCommandValidator : AbstractValidator<UpdateProfileUserCommand>
    {
        public UpdateProfileUserCommandValidator()
        {
            RuleFor(command => command.FullName)
                .NotEmpty().WithMessage("الاسم الكامل مطلوب")
                .MaximumLength(100).WithMessage("الاسم الكامل لا يجب أن يتجاوز 100 حرف");
            RuleFor(command => command.Email)
                .NotEmpty().WithMessage("البريد الإلكتروني مطلوب")
                .EmailAddress().WithMessage("البريد الإلكتروني غير صالح");
            RuleFor(command => command.PhoneNumber)
                .NotEmpty().WithMessage("رقم الهاتف مطلوب")
                .Matches(@"^\+?\d{10,15}$").WithMessage("رقم الهاتف غير صالح");
            RuleFor(command => command.Address)
                .NotEmpty().WithMessage("العنوان مطلوب")
                .MaximumLength(200).WithMessage("العنوان لا يجب أن يتجاوز 200 حرف");

        }
    }
}
