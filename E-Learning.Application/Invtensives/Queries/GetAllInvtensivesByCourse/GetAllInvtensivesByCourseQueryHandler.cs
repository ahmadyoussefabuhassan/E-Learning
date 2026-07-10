using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Courses;
using E_Learning.Domain.Invtensives;

namespace E_Learning.Application.Invtensives.Queries.GetAllInvtensivesByCourse
{
    public sealed class GetAllInvtensivesByCourseQueryHandler : IQueryHandler<GetAllInvtensivesByCourseQuery, IEnumerable<InvtensiveResponse>>
    {
        private readonly IInvtensivesRepositry _invtensivesRepositry;
        private readonly ICourseRepository _courseRepository;

        public GetAllInvtensivesByCourseQueryHandler(IInvtensivesRepositry invtensivesRepositry, ICourseRepository courseRepository)
        {
            _invtensivesRepositry = invtensivesRepositry;
            _courseRepository = courseRepository;
        }

        public async Task<Result<IEnumerable<InvtensiveResponse>>> Handle(GetAllInvtensivesByCourseQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.CourseId, cancellationToken);
            if (course is null)
                return Result.Failure<IEnumerable<InvtensiveResponse>>(CourseErrors.NotFound);
            var invtensives = await _invtensivesRepositry.GetAllInvtensivesByCourseAsync(course.Id, cancellationToken);
            if(!invtensives.Any())
                return Result.Success(Enumerable.Empty<InvtensiveResponse>());
            var response = invtensives.Select(inv => new InvtensiveResponse(
                inv.Id,
                inv.Title.Value,
                inv.Description.Value,
                inv.Price.Value
            ));
            return Result.Success(response);
        }
    }
}
