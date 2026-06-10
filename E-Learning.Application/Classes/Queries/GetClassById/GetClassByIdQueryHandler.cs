using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Classes;

namespace E_Learning.Application.Classes.Queries.GetClassById
{
    public sealed class GetClassByIdQueryHandler : IQueryHandler<GetClassByIdQuery, ClassResponse>
    {
        private readonly IClassesRepositry _classesRepositry;

        public GetClassByIdQueryHandler(IClassesRepositry classesRepositry)
        {
            _classesRepositry = classesRepositry;
        }

        public async Task<Result<ClassResponse>> Handle(GetClassByIdQuery request, CancellationToken cancellationToken)
        {
            var classEntity = await _classesRepositry.GetByIdAsync(request.Id, cancellationToken);
            if (classEntity is null)
                return Result.Failure<ClassResponse>(ClassesErrors.NotFound);
            var response = new ClassResponse(classEntity.Id, classEntity.Name.Value);
            return Result.Success(response);
        }
    }
}
