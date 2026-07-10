using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Teachers.Queries.GetAllTeachers
{
    public sealed record GetAllTeachersQuery() : IQuery<IEnumerable<TeachersResponse>>;
}
