using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Application.Courses.Queries.SherdResponses;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Classes;
using E_Learning.Domain.Courses;
using E_Learning.Domain.Students;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.Courses.Queries.GetAllCoursesFilterByClass
{
    public sealed class GetAllCoursesFilterByClassQueryHandler : BaseService,IQueryHandler<GetAllCoursesFilterByClassQuery, IEnumerable<CourseResponse>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IClassesRepositry _classesRepositry;


        public GetAllCoursesFilterByClassQueryHandler(IStudentRepository studentRepository,
            ICourseRepository courseRepository,
            IHttpContextAccessor httpContextAccessor,
            IClassesRepositry classesRepositry) : base(httpContextAccessor)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _classesRepositry = classesRepositry;
        }

        public async Task<Result<IEnumerable<CourseResponse>>> Handle(GetAllCoursesFilterByClassQuery request, CancellationToken cancellationToken)
        {
            var studentId = UserId;
            var student = await _studentRepository.GetByIdAsync(studentId, cancellationToken);
            if(student is null)
                return Result.Failure<IEnumerable<CourseResponse>>(UserErrors.Unauthorized);
            var classes = await _classesRepositry.GetClassesByNameAsync(new ClassesName(student.SubjectStudent.Value),
                cancellationToken);
            if(classes is null)
                return Result.Failure<IEnumerable<CourseResponse>>(ClassesErrors.NotFound);
            var courses = await _courseRepository.GetAllByClassesAsync(classes.Id, cancellationToken);
            var response = courses.Select(course => new CourseResponse(
                course.Id,
                course.CourseName.Value,
                course.Description.Value,
                course.Price.Value,
                course.ImageUrl.Value,
                course.Classes.Name.Value,
                course.Teachers.FullName.Value
            ));
            return Result.Success(response);

        }
    }
}
