using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Courses;

namespace E_Learning.Application.Courses.Queries.GetCourseByIdForStudent
{
    public sealed class GetCourseByIdForStudentQueryHandler : IQueryHandler<GetCourseByIdForStudentQuery, CourseResponse>
    {
        private readonly ICourseRepository _courseRepository;

        public GetCourseByIdForStudentQueryHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<Result<CourseResponse>> Handle(GetCourseByIdForStudentQuery request, CancellationToken cancellationToken)
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
                course.Teachers.FullName.Value,
                course.IsLocked
            );
            return Result.Success(response);
        }
    }
}
