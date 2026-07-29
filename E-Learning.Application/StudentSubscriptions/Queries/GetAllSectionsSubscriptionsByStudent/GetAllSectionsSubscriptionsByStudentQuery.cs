using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.StudentSubscriptions.Queries.GetAllSectionsSubscriptionsByStudent
{
    public sealed record GetAllSectionsSubscriptionsByStudentQuery() : IQuery<IEnumerable<SectionResponse>>;
}
