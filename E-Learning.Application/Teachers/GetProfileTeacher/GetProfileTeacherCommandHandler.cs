using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Roles;
using E_Learning.Domain.Teachers;
using E_Learning.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Application.Teachers.GetProfileTeacher
{
    public sealed class GetProfileTeacherCommandHandler : BaseService, IQueryHandler<GetProfileTeacherCommand, TeacherResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITeacherRepository _teacherRepository;

        public GetProfileTeacherCommandHandler(IUserRepository userRepository, ITeacherRepository teacherRepository)
        {
            _userRepository = userRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task<Result<TeacherResponse>> Handle(GetProfileTeacherCommand request, CancellationToken cancellationToken)
        {
            Guid currentUserId = UserId;
            var teacher = await _teacherRepository.GetByIdAsync(currentUserId, cancellationToken);
            if (teacher is null)
                return Result.Failure<TeacherResponse>(TeacherErrors.NotFound);
            var user = teacher.User;
            if (user is null)
                return Result.Failure<TeacherResponse>(UserErorrs.NotFound);
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
