using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Courses;
using E_Learning.Domain.Sections;

namespace E_Learning.Application.Sections.Queries.GetAllSectionsByCourse
{
    public sealed class GetAllSectionsByCourseQueryHandler : IQueryHandler<GetAllSectionsByCourseQuery, IEnumerable<SectionResponse>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ISectionRepository _sectionRepository;

        public GetAllSectionsByCourseQueryHandler(ISectionRepository sectionRepository, ICourseRepository courseRepository)
        {
            _sectionRepository = sectionRepository;
            _courseRepository = courseRepository;
        }

        public async Task<Result<IEnumerable<SectionResponse>>> Handle(GetAllSectionsByCourseQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.courseId, cancellationToken);
            if (course is null)
                return Result.Failure<IEnumerable<SectionResponse>>(CourseErrors.NotFound);
            var sections = await _sectionRepository.GetAllByCourseAsync(request.courseId, cancellationToken);
            if (!sections.Any())
                return Result.Success(Enumerable.Empty<SectionResponse>());
            var response = sections.Select(section => new SectionResponse(
                section.Id,
                section.SectionTitle.Value,
                section.Price.Value
            ));
            return Result.Success(response);
        }
    }
}
