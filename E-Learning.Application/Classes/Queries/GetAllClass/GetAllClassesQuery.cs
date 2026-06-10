using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Classes.Queries.GetAllClass
{
    public sealed record GetAllClassesQuery() :  IQuery<IEnumerable<ClassResponse>>;
}
