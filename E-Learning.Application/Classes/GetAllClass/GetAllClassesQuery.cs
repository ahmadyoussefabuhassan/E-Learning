using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Classes.GetAllClass
{
    public sealed record GetAllClassesQuery() :  IQuery<IEnumerable<ClassResponse>>;
}
