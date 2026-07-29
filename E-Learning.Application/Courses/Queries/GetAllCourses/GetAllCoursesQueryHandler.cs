using E_Learning.Application.Abstractions.Extensions;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Courses.Queries.SherdResponses;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Courses;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Application.Courses.Queries.GetAllCourses
{
    public sealed class GetAllCoursesQueryHandler : IQueryHandler<GetAllCoursesQuery, GetAllDataResponse<CourseResponse>>
    {
        private readonly ICourseRepository _courseRepository;

        public GetAllCoursesQueryHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<Result<GetAllDataResponse<CourseResponse>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var query =  _courseRepository.GetAllQueryable(cancellationToken);
            query = query.Include(c => c.Classes).Include(c => c.Teachers);
            if(request.ClassId.HasValue)
                query = query.Where(x => x.ClassesId == request.ClassId.Value);
            if(request.TeacherId.HasValue)
                query = query.Where(x => x.TeacherId == request.TeacherId.Value);
            if(request.CourseId.HasValue)
                query = query.Where(x => x.Id == request.CourseId.Value);
            var response = await query.ToPagedResponseAsync(
                    request.PageNumber,
                     request.PageSize,
                      course => new CourseResponse(
                            course.Id,
                            course.CourseName.Value,
                            course.Description.Value,
                            course.Price.Value,
                            course.ImageUrl.Value,
                            course.Classes.Name.Value,
                            course.Teachers.FullName.Value
                      )
            );
            return Result.Success(response);
        }
    }
}
