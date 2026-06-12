using E_Learning.Application.Abstractions.Messaging;


namespace E_Learning.Application.Students.Queries.GetCountStudents
{
    public sealed record GetCountStudentsQuery() : IQuery<int>;

}
