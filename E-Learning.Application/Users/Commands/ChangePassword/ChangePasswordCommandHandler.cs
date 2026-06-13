

using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.Users.Commands.ChangePassword
{
    public sealed class ChangePasswordCommandHandler : BaseService, ICommandHandler<ChangePasswordCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public ChangePasswordCommandHandler(IUnitOfWork unitOfWork
            , IUserRepository userRepository
            , IHttpContextAccessor httpContextAccessor) 
            : base(httpContextAccessor) 
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<Result<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = UserId;
            var user = await _userRepository.GetByIdAsync(currentUserId, cancellationToken);
            if (user is null)
                return Result.Failure<bool>(UserErrors.NotFound);
            if (user.Password.Value != request.OldPassword)
                return Result.Failure<bool>(UserErrors.InvalidOldPassword);
            if(request.NewPassword != request.ChekPassword)
                return Result.Failure<bool>(UserErrors.InvalidPassword);
            user.ChangePassword(
                new Password(request.NewPassword)
            );
            await _userRepository.UpdateAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(true);
        }
    }
}
