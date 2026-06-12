using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Students;

namespace E_Learning.Application.Students.Queries.GetCountStudents
{
    public sealed class GetCountStudentsQueryHandler : IQueryHandler<GetCountStudentsQuery, int>
    {
        private readonly IStudentRepository _studentRepository;

        public GetCountStudentsQueryHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Result<int>> Handle(GetCountStudentsQuery request, CancellationToken cancellationToken)
        {
            var count = await _studentRepository.GetCountStudentsAsync(cancellationToken);
            if(count == 0)
                return Result.Success(0);
            return Result.Success(count);
        }
    }
}
