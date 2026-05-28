using E_Learning.Application.Abstractions.Authentication;
using E_Learning.Application.Abstractions.Clock;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.RefreshTokens;
using E_Learning.Domain.Roles;
using E_Learning.Domain.User;

namespace E_Learning.Application.Users.LogInUser
{
    public sealed class LogInUserCommandHandler : ICommandHandler<LogInUserCommand, AuthenticationResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public LogInUserCommandHandler(IUnitOfWork unitOfWork, IJwtService jwtService, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, IRoleRepository roleRepository, IDateTimeProvider dateTimeProvider)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _roleRepository = roleRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Result<AuthenticationResponse>> Handle(LogInUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(new Email(request.Email), cancellationToken);
            if (user is null)
                return Result.Failure<AuthenticationResponse>(UserErorrs.InvalidCredentials);
            var role = await _roleRepository.GetByIdAsync(user.RoleId, cancellationToken);
            if(role is null)
                return Result.Failure<AuthenticationResponse>(RoleErrors.NotFound);
            if (role.Name != Name.Admin|| role.Name != Name.Teacher)
                return Result.Failure<AuthenticationResponse>(UserErorrs.Unauthorized);
            var token = _jwtService.GenerateToken(user.Id, user.Email.Value, role.Name.Value);
            var refreshTokenText = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            var refreshToken = RefreshToken.Create(
                refreshTokenText,
                Guid.NewGuid().ToString(),
                _dateTimeProvider.UtcNow,
                _dateTimeProvider.UtcNow.AddDays(7),
                user.Id
            );
            await _refreshTokenRepository.AddSaveToken(refreshToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var response = new AuthenticationResponse(token, refreshToken.Token, user.Id);
            return Result.Success(response);


        }
    }
}
