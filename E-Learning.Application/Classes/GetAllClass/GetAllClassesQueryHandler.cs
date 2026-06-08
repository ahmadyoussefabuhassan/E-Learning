

using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Classes;

namespace E_Learning.Application.Classes.GetAllClass
{
    public sealed class GetAllClassesQueryHandler : IQueryHandler<GetAllClassesQuery, IEnumerable<ClassResponse>>
    {
        private readonly IClassesRepositry _classesRepositry;

        public GetAllClassesQueryHandler(IClassesRepositry classesRepositry)
        {
            _classesRepositry = classesRepositry;
        }

        public async Task<Result<IEnumerable<ClassResponse>>> Handle(GetAllClassesQuery request, CancellationToken cancellationToken)
        {
            var classes = await _classesRepositry.GetAllAsync(cancellationToken);
            if (classes is null || !classes.Any())
                return Result.Failure<IEnumerable<ClassResponse>>(ClassesErrors.NotFound);
            var response = classes.Select(c => new ClassResponse(c.Id, c.Name.Value));
            return Result.Success(response);
        }
    }
}
