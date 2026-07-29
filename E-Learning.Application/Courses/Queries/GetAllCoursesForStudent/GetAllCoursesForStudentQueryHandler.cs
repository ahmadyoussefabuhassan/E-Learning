using E_Learning.Application.Abstractions.Extensions;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Courses;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Application.Courses.Queries.GetAllCoursesForStudent
{
    public sealed class GetAllCoursesForStudentQueryHandler : IQueryHandler<GetAllCoursesForStudentQuery, GetAllDataResponse<CourseResponse>>
    {
        private readonly ICourseRepository _courseRepository;

        public GetAllCoursesForStudentQueryHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<Result<GetAllDataResponse<CourseResponse>>> Handle(GetAllCoursesForStudentQuery request, CancellationToken cancellationToken)
        {
            var query = await _courseRepository.GetAllQueryableAsync(cancellationToken);
            query = query.Include(c => c.Classes)
                 .Include(c => c.Teachers);
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var search = request.SearchTerm.Trim().ToLower();

                query = query.Where(x =>
                    x.CourseName.Value.ToLower().Contains(search) ||
                    x.Teachers.FullName.Value.ToLower().Contains(search) ||
                    x.Classes.Name.Value.ToLower().Contains(search));
            }
            query = query.OrderByDescending(x => x.Id);
            var response = await query.ToPagedResponseAsync(
                request.PageNumber,
                request.PageSize,
                course => new CourseResponse(
                    course.Id,
                    course.CourseName.Value,
                    course.Description.Value,
                    course.Price.Value,
                    course.ImageUrl?.Value ?? string.Empty,
                    course.Classes.Name.Value,
                    course.Teachers.FullName.Value,
                    course.IsLocked
                )
            );
            return Result.Success(response);
        }
    }
}
