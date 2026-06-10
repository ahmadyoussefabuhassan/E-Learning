using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Classes.Queries.GetClassById
{
    public sealed record GetClassByIdQuery(Guid Id) : IQuery<ClassResponse>;
}
