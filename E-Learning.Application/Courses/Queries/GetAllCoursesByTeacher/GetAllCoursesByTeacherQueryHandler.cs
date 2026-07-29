using E_Learning.Application.Abstractions.Extensions;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Classes;
using E_Learning.Domain.Courses;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Application.Courses.Queries.GetAllCoursesByTeacher
{
    public sealed class GetAllCoursesByTeacherQueryHandler : BaseService, IQueryHandler<GetAllCoursesByTeacherQuery, GetAllDataResponse<CoursesResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;

        public GetAllCoursesByTeacherQueryHandler(IUserRepository userRepository, 
            ICourseRepository courseRepository,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _userRepository = userRepository;
            _courseRepository = courseRepository;
        }

        public async Task<Result<GetAllDataResponse<CoursesResponse>>> Handle(GetAllCoursesByTeacherQuery request, CancellationToken cancellationToken)
        {
            Guid currentUserId = UserId;
            var user = await _userRepository.GetByIdAsync(currentUserId, cancellationToken);
            if (user is null)
                return Result.Failure<GetAllDataResponse<CoursesResponse>>(UserErrors.NotFound);
            if (user.Role.notType != Domain.Roles.NotType.Teacher)
                return Result.Failure<GetAllDataResponse<CoursesResponse>>(UserErrors.Unauthorized);
            var query =  _courseRepository.GetAllQueryable(cancellationToken);
            query = query.Include(c => c.Classes)
                .Where(c => c.TeacherId == user.Id);
            if (!string.IsNullOrWhiteSpace(request.Query))
                query = query.Where(c => c.CourseName.Value.Contains(request.Query));
            if(request.CourseId.HasValue)
                query = query.Where(c => c.Id == request.CourseId.Value);
            var response = await query.ToPagedResponseAsync(
             request.PageNumber,
             request.PageSize,
             course => new CoursesResponse(
                 course.Id,
                 course.CourseName.Value,
                 course.Description.Value,
                 course.Price.Value,
                 course.ImageUrl.Value,
                 course.Classes.Name.Value 
             )
            );
            return Result.Success( response );
        }
    }
}
