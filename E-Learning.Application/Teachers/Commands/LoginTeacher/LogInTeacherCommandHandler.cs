using E_Learning.Application.Abstractions.Authentication;
using E_Learning.Application.Abstractions.Clock;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.RefreshTokens;
using E_Learning.Domain.Roles;
using E_Learning.Domain.User;

namespace E_Learning.Application.Teachers.Commands.LoginTeacher
{
    public sealed class LogInTeacherCommandHandler : ICommandHandler<LogInTeacherCommand, AuthenticationResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _jwtService;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public LogInTeacherCommandHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtService,
            IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IDateTimeProvider dateTimeProvider)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Result<AuthenticationResponse>> Handle(LogInTeacherCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(new Email(request.Email), cancellationToken);
            if (user is null || user.Password.Value != request.Password)
                return Result.Failure<AuthenticationResponse>(UserErrors.InvalidCredentials);
            if (user.Role is null)
                return Result.Failure<AuthenticationResponse>(RoleErrors.NotFound);
            string jit = Guid.NewGuid().ToString();
            var token = _jwtService.GenerateToken(user.Id, user.Email.Value, user.FullName.Value, user.Role.Name.Value, jit);
            var refreshTokenText = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            var refreshToken = RefreshToken.Create(
                refreshTokenText,
                jit,
                _dateTimeProvider.Now,
                _dateTimeProvider.Now.AddDays(7),
                user.Id
            );
            await _refreshTokenRepository.AddSaveToken(refreshToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var response = new AuthenticationResponse(token, refreshToken.Token, user.Id);
            return Result.Success(response);
        }
    }
}
