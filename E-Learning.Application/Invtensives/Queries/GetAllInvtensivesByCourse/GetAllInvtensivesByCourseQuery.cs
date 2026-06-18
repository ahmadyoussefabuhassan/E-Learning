using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Invtensives.Queries.GetAllInvtensivesByCourse
{
    public sealed record GetAllInvtensivesByCourseQuery(Guid CourseId) : IQuery<IEnumerable<InvtensiveResponse>>;
}
