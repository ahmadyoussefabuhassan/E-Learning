using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.StudentSubscriptions.Commands.AcceptStudent
{
    public sealed record AcceptSubscriptionCommand(Guid SubscriptionId) : ICommand<Guid>;

}
