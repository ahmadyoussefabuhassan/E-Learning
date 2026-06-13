using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.User;

namespace E_Learning.Application.Users.Commands.ChangePasswordResetCode
{
    public sealed class ChangePasswordResetCodeCommandHandler : ICommandHandler<ChangePasswordResetCodeCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangePasswordResetCodeCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(ChangePasswordResetCodeCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetResetCodeAsync(request.code, cancellationToken);
            if (user is null)
                return Result.Failure<string>(UserErrors.InvalidResetCode);
            user.ChangePassword(
                new Password(request.Password)
            );
            user.ClearResetCode();
            await _userRepository.UpdateAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success("تم تغير كلمة المرور يرجى تسجيل الدخول لان");
        }
    }
}
