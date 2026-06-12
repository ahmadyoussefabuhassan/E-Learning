using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Courses.Queries.SherdResponses;

namespace E_Learning.Application.Courses.Queries.GetAllCoursesFilterByClass
{
    public sealed record GetAllCoursesFilterByClassQuery() : IQuery<IEnumerable<CourseResponse>>;
}
