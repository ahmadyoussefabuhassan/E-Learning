using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.RefreshTokens;

namespace E_Learning.Application.Users.LogOut
{
    public sealed class LogOutUserCommandHandler : ICommandHandler<LogOutUserCommand, bool>
    {
        private readonly IRefreshTokenRepository _refreshTokensRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LogOutUserCommandHandler(IRefreshTokenRepository refreshTokensRepository, IUnitOfWork unitOfWork)
        {
            _refreshTokensRepository = refreshTokensRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(LogOutUserCommand request, CancellationToken cancellationToken)
        {
            var token = await _refreshTokensRepository.GetToken(request.token);
            if (token is null)
                 return Result.Failure<bool>(RefreshTokenErrors.NotFound);
            await _refreshTokensRepository.DeleteToken(request.token);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(true);
        }
    }
}
