using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Teachers.Queries.GetCountTeachers
{
    public sealed record  GetCountTeachersQuery() : IQuery<int>;

}
