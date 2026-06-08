using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Classes.GetClassById
{
    public sealed record GetClassByIdQuery(Guid Id) : IQuery<ClassResponse>;
}
