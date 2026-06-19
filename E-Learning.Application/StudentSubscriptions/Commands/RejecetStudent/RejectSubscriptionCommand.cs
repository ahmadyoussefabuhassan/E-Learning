using E_Learning.Application.Abstractions.Messaging;


namespace E_Learning.Application.StudentSubscriptions.Commands.RejecetStudent
{
    public sealed record RejectSubscriptionCommand(Guid SubscriptionId) : ICommand<Guid>;
}
