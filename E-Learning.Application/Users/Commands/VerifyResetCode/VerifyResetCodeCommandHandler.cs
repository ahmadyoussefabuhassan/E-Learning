using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.User;


namespace E_Learning.Application.Users.Commands.VerifyResetCode
{
    public sealed class VerifyResetCodeCommandHandler : ICommandHandler<VerifyResetCodeCommand, bool>
    {
        private IUserRepository _userRepository;

        public VerifyResetCodeCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<bool>> Handle(VerifyResetCodeCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(new Email(request.Email), cancellationToken);
            if (user is null)
                return Result.Failure<bool>(UserErrors.NotFound);
            if (user.PasswordResetCode is null || string.IsNullOrEmpty(user.PasswordResetCode.Value))
                return Result.Failure<bool>(UserErrors.InvalidResetCode);
            
            if(user.PasswordResetCode.Value != request.Code)
                return Result.Failure<bool>(UserErrors.InvalidResetCode);
            if(user.PasswordResetCodeExpiresAt < DateTime.UtcNow)
                return Result.Failure<bool>(UserErrors.ResetCodeExpired);
            return Result.Success(true);
        }
    }
}
