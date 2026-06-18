using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Invtensives.Queries.GetInvtensivesById
{
    public sealed record GetInvtensivesByIdQuery(Guid Id) : IQuery<InvtensiveResponse>;
}
