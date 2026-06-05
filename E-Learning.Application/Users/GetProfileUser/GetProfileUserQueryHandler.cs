using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Roles;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.Users.GetProfileUser
{
    public sealed class GetProfileUserQueryHandler : BaseService, IQueryHandler<GetProfileUserQuery, UserResponse>
    {
        private readonly IUserRepository _userRepository;
 
        public GetProfileUserQueryHandler(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UserResponse>> Handle(GetProfileUserQuery request, CancellationToken cancellationToken)
        {
            Guid currentUserId = UserId;
            var user = await _userRepository.GetByIdAsync(currentUserId, cancellationToken);
            if(user is null)
                return Result.Failure<UserResponse>(UserErorrs.NotFound);
            var response = new UserResponse(
                user.FullName.Value,
                user.Email.Value,
                user.PhoneNumber.Value,
                user.Address.Value,
                user.ImageUrl?.Value ?? "/uploads/users/default-profile.png",
                user.Role?.notType.ToArabicString() ?? string.Empty
            );
            return Result.Success(response);
        }
    }
}
