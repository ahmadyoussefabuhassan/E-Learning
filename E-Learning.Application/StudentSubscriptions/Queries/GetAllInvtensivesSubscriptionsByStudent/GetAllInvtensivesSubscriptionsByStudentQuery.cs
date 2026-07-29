using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.StudentSubscriptions.Queries.GetAllInvtensivesSubscriptionsByStudent
{
    public sealed record GetAllInvtensivesSubscriptionsByStudentQuery() : IQuery<IEnumerable<InvtensiveResponse>>;
}
