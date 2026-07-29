using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Courses;
using E_Learning.Domain.StudentSubscription;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.StudentSubscriptions.Queries.GetAllCoursesSubscriptionsByStudent
{
    public sealed class GetAllCoursesSubscriptionsByStudentQueryHandler : BaseService, IQueryHandler<GetAllCoursesSubscriptionsByStudentQuery, IEnumerable<CourseResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IStudentSubscriptionRepositry _studentSubscriptionRepositry;
        public GetAllCoursesSubscriptionsByStudentQueryHandler(IUserRepository userRepository, IStudentSubscriptionRepositry studentSubscriptionRepositry 
            , IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) 
        {
            _userRepository = userRepository;
            _studentSubscriptionRepositry = studentSubscriptionRepositry;
        }

        public async Task<Result<IEnumerable<CourseResponse>>> Handle(GetAllCoursesSubscriptionsByStudentQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(UserId, cancellationToken);
            if(user is null) 
                return Result.Failure<IEnumerable<CourseResponse>>(UserErrors.NotFound);
            if(user.Role.notType != Domain.Roles.NotType.Student)
                return Result.Failure<IEnumerable<CourseResponse>>(UserErrors.Unauthorized);
            var courses = await _studentSubscriptionRepositry.GetAllCourseSubscribersAsync(user.Id, cancellationToken);
            if(courses is null || !courses.Any())
                return Result.Success<IEnumerable<CourseResponse>>(Enumerable.Empty<CourseResponse>());
            var response = courses.Select(c => new CourseResponse(
                c.Id,
                c.CourseName.Value,
                c.Description.Value,
                c.Price.Value,
                c.ImageUrl?.Value ?? string.Empty,
                c.Classes.Name.Value,
                c.Teachers.FullName.Value,
                c.IsLocked
            ));
            return Result.Success(response);



        }
    }
}
