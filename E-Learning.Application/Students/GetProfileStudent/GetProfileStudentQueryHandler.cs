using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Roles;
using E_Learning.Domain.Students;
using E_Learning.Domain.User;

namespace E_Learning.Application.Students.GetProfileStudent
{
    public sealed class GetProfileStudentQueryHandler : BaseService, IQueryHandler<GetProfileStudentQuery, StudentResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;

        public GetProfileStudentQueryHandler(IUserRepository userRepository, IStudentRepository studentRepository)
        {
            _userRepository = userRepository;
            _studentRepository = studentRepository;
        }

        public async Task<Result<StudentResponse>> Handle(GetProfileStudentQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = UserId;
            var user = await _userRepository.GetByIdAsync(currentUserId, cancellationToken);
            if (user is null)
                return Result.Failure<StudentResponse>(UserErorrs.NotFound);
            var student = await _studentRepository.GetByIdAsync(user.Id, cancellationToken);
            if (student is null)
                return Result.Failure<StudentResponse>(StudentErrors.NotFound);
            var response = new StudentResponse(
                user.FullName.Value,
                user.Email.Value,
                user.PhoneNumber.Value,
                user.Address.Value,
                 user.ImageUrl?.Value ?? "/uploads/users/default-profile.png",
                student.SubjectStudent.Value,
                user.Role?.notType.ToArabicString() ?? string.Empty
            );
            return Result.Success(response);
        }
    }
}
