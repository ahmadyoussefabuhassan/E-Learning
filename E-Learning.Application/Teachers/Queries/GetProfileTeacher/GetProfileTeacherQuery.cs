using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Teachers.Queries.GetProfileTeacher
{
    public sealed record GetProfileTeacherQuery() : IQuery<TeacherResponse>;
}
