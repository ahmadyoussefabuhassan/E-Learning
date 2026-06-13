

using E_Learning.Application.Abstractions.Email;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.User;

namespace E_Learning.Application.Users.Commands.ForgotPassword
{
    public sealed class SendResetCodeCommandHandler : ICommandHandler<SendResetCodeCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;

        public SendResetCodeCommandHandler(IUserRepository userRepository,
            IEmailService emailService,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(SendResetCodeCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(new Email(request.Email), cancellationToken);
            if (user is null)
                return Result.Failure(UserErrors.InvalidEmail);
            user.GenerateResetCode();
            await _userRepository.UpdateAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            string subject = "رمز استعادة كلمة المرور - E-learning";
            var body = $@"
                <div style='direction: rtl; font-family: Tahoma; text-align: center; border: 1px solid #ddd; padding: 20px;'>
                    <h2 style='color: #2D6A4F;'>مرحباً {user.FullName.Value}</h2>
                    <p>لقد طلبت رمزاً لإعادة تعيين كلمة المرور الخاصة بك.</p>
                    <div style='background-color: #f9f9f9; padding: 15px; font-size: 28px; font-weight: bold; letter-spacing: 5px; border-radius: 10px; color: #1B4332;'>
                        {user.PasswordResetCode?.Value}
                    </div>
                    <p style='color: #666;'>هذا الرمز صالح لمدة 15 دقيقة فقط.</p>
                    <p>إذا لم تطلب هذا الرمز، يرجى تجاهل هذا الإيميل.</p>
                </div>"
            ;
            await _emailService.SendEmailAsync(user.Email.Value ,subject,body , cancellationToken);
            return Result.Success();
        }
    }
}
