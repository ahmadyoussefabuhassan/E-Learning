
using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Invtensives.Queries.GetInvtensivesByIdForStudent
{
    public sealed record GetInvtensivesByIdForStudentQuery(Guid Id) : IQuery<InvtensiveResponse>;
}
