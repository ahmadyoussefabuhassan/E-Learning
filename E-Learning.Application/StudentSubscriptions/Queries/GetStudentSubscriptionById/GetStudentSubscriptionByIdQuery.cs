using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.StudentSubscriptions.Queries.GetStudentSubscriptionById
{
    public sealed record GetStudentSubscriptionByIdQuery(Guid Id) : IQuery<StudentSubscriptionResponse>;
}
