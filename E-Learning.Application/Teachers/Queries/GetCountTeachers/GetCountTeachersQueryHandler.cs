using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Teachers;

namespace E_Learning.Application.Teachers.Queries.GetCountTeachers
{
    public sealed class GetCountTeachersQueryHandler : IQueryHandler<GetCountTeachersQuery, int>
    {
        private readonly ITeacherRepository _teacherRepository;

        public GetCountTeachersQueryHandler(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<Result<int>> Handle(GetCountTeachersQuery request, CancellationToken cancellationToken)
        {
            var count = await _teacherRepository.GetCountTeachersAsync(cancellationToken);
            if (count is 0)
                return Result.Success(0);
            return Result.Success(count);
        }
    }
}
