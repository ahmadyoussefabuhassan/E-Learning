using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Teachers;

namespace E_Learning.Application.Teachers.Queries.GetAllTeachers
{
    public sealed class GetAllTeachersQueryHandler : IQueryHandler<GetAllTeachersQuery, IEnumerable<TeachersResponse>>
    {
        private readonly ITeacherRepository _teacherRepository;

        public GetAllTeachersQueryHandler(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<Result<IEnumerable<TeachersResponse>>> Handle(GetAllTeachersQuery request, CancellationToken cancellationToken)
        {
            var teachers = await _teacherRepository.GetAllAsync(cancellationToken);
            if(!teachers.Any())
                return Result.Success(Enumerable.Empty<TeachersResponse>());
            var response = teachers.Select(t => new TeachersResponse(
                t.Id,
                t.User.FullName.Value,
                t.User.Email.Value,
                t.User.PhoneNumber.Value,
                t.User.Address.Value,
                t.User.ImageUrl?.Value ?? string.Empty,
                t.SubjectTeacher.Value,
                t.UrlShamCash?.Value ?? string.Empty
            ));
            return Result.Success(response);
        }
    }
}
