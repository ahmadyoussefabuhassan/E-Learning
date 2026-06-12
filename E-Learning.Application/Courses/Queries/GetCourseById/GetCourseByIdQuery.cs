using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Courses.Queries.SherdResponses;


namespace E_Learning.Application.Courses.Queries.GetCourseById
{
    public sealed record GetCourseByIdQuery(Guid Id) : IQuery<CourseResponse>;
}
