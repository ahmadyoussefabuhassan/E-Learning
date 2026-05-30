using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Teachers.GetProfileTeacher
{
    public sealed record GetProfileTeacherQuery(Guid userId) : IQuery<TeacherResponse>;
}
