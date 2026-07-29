using E_Learning.Application.Abstractions.Messaging;


namespace E_Learning.Application.Courses.Queries.GetAllCoursesByTeacherId
{
    public sealed record GetAllCoursesByTeacherIdQuery(Guid teacherId) : IQuery<IEnumerable<CoursesResponse>>;
}
