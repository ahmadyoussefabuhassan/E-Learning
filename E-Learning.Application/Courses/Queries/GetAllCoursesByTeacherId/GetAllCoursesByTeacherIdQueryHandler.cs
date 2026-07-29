using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Courses;
using E_Learning.Domain.User;

namespace E_Learning.Application.Courses.Queries.GetAllCoursesByTeacherId
{
    public sealed class GetAllCoursesByTeacherIdQueryHandler : IQueryHandler<GetAllCoursesByTeacherIdQuery, IEnumerable<CoursesResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;

        public GetAllCoursesByTeacherIdQueryHandler(IUserRepository userRepository, ICourseRepository courseRepository)
        {
            _userRepository = userRepository;
            _courseRepository = courseRepository;
        }

        public async Task<Result<IEnumerable<CoursesResponse>>> Handle(GetAllCoursesByTeacherIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.teacherId , cancellationToken);
            if (user is null)
                return Result.Failure<IEnumerable<CoursesResponse>>(UserErrors.NotFound);
            var courses = await _courseRepository.GetAllByTeacherId(user.Id, cancellationToken);
            if(! courses.Any())
                return Result.Success<IEnumerable<CoursesResponse>>(Enumerable.Empty<CoursesResponse>());
            var response = courses.Select(c => new CoursesResponse(
                c.Id,
                c.CourseName.Value,
                c.Description.Value,
                c.Price.Value,
                c.ImageUrl?.Value ?? string.Empty,
                c.Classes.Name.Value
            ));
            return Result.Success(response);
        }
    }
}
