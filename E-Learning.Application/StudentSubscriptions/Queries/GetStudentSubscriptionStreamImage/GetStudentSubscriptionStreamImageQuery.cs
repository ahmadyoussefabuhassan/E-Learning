using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.StudentSubscriptions.Queries.GetStudentSubscriptionStreamImage
{
    public sealed record GetStudentSubscriptionStreamImageQuery(
        Guid StudentSubscriptionId
    ) : IQuery<FileStream>;
}
