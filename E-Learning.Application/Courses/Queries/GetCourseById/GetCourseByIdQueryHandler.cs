using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Courses.Queries.SherdResponses;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Courses;

namespace E_Learning.Application.Courses.Queries.GetCourseById
{
    public sealed class GetCourseByIdQueryHandler : IQueryHandler<GetCourseByIdQuery, CourseResponse>
    {
        private readonly ICourseRepository _courseRepository;

        public GetCourseByIdQueryHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<Result<CourseResponse>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.Id, cancellationToken);
            if (course is null)
                return Result.Failure<CourseResponse>(CourseErrors.NotFound);
            var response = new CourseResponse(
                course.Id,
                course.CourseName.Value,
                course.Description.Value,
                course.Price.Value,
                course.ImageUrl.Value,
                course.Classes.Name.Value,
                course.Teachers.FullName.Value
            );
            return Result.Success( response );
        }
    }
}
