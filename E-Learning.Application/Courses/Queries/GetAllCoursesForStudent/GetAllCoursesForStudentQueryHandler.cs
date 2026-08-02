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
            var allCourses = await _courseRepository.GetAllQueryable()
                  .Include(c => c.Classes)
                  .Include(c => c.Teachers)
                  .OrderByDescending(x => x.Id)
                  .ToListAsync(cancellationToken);

            var filteredCourses = allCourses.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var search = request.SearchTerm.Trim().ToLower();

                filteredCourses = filteredCourses.Where(x =>
                    (x.CourseName?.Value != null && x.CourseName.Value.ToLower().Contains(search)) ||
                    (x.Teachers?.FullName?.Value != null && x.Teachers.FullName.Value.ToLower().Contains(search)) ||
                    (x.Classes?.Name?.Value != null && x.Classes.Name.Value.ToLower().Contains(search))
                );
            }

            var totalCount = filteredCourses.Count();
            var pagedData = filteredCourses
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(course => new CourseResponse(
                    course.Id,
                    course.CourseName?.Value ?? "بدون عنوان",
                    course.Description?.Value ?? string.Empty,
                    course.Price?.Value ?? 0,
                    course.ImageUrl?.Value ?? string.Empty,
                    course.Classes?.Name?.Value ?? "غير محدد",
                    course.Teachers?.FullName?.Value ?? "أستاذ غير معروف",
                    course.IsLocked 
                ))
                .ToList();

            var response = new GetAllDataResponse<CourseResponse>
            {
                PageNumber = request.PageNumber,
                TotalDataCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize),
                Data = pagedData
            };

            return Result.Success(response);
        }
    }
}
