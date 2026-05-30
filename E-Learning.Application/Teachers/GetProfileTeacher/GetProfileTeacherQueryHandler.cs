using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Roles;
using E_Learning.Domain.Teachers;
using E_Learning.Domain.User;


namespace E_Learning.Application.Teachers.GetProfileTeacher
{
    public sealed class GetProfileTeacherQueryHandler : BaseService, IQueryHandler<GetProfileTeacherQuery, TeacherResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITeacherRepository _teacherRepository;

        public GetProfileTeacherQueryHandler(IUserRepository userRepository, ITeacherRepository teacherRepository)
        {
            _userRepository = userRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task<Result<TeacherResponse>> Handle(GetProfileTeacherQuery request, CancellationToken cancellationToken)
        {
            Guid currentUserId = UserId;
            var user = await _userRepository.GetByIdAsync(currentUserId, cancellationToken);
            if (user is null)
                return Result.Failure<TeacherResponse>(UserErorrs.NotFound);
            var teacher = await _teacherRepository.GetByIdAsync(user.Id, cancellationToken);
            if (teacher is null)
                return Result.Failure<TeacherResponse>(TeacherErrors.NotFound);
            var response = new TeacherResponse(
                user.FullName.Value,
                user.Email.Value,
                user.PhoneNumber.Value,
                user.Address.Value,
                user.ImageUrl?.Value ?? "/uploads/users/default-profile.png",
                teacher.UrlShamCash?.Value ?? string.Empty,
                teacher.SubjectTeacher.Value,
                user.Role?.notType.ToArabicString()?? string.Empty
            );
            return Result.Success(response);

        }
    }
}
