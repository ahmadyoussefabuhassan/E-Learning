using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Courses.Queries.GetCourseByIdForStudent
{
    public sealed record GetCourseByIdForStudentQuery(Guid Id) : IQuery<CourseResponse>;
}
